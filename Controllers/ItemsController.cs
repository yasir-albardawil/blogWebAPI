using AutoMapper;
using BiteWebAPI.DTOs;
using BiteWebAPI.Entities;
using BiteWebAPI.Models;
using BiteWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiteWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public ItemsController(IItemRepository itemRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems(string? search, string? sortOrder, bool includeItemsOfTheWeak)
        {
            try
            {
                var items = await _itemRepository.GetItemsAsync();

                if (!string.IsNullOrEmpty(search))
                {
                    items = items.Where(p =>
                        p.Name.Contains(search) ||
                        p.LongDescription.Contains(search));
                }

                items = sortOrder switch
                {
                    "name_desc" => items.OrderByDescending(p => p.Name),
                    "id_desc" => items.OrderByDescending(p => p.Id),
                    _ => items.OrderBy(p => p.Name),
                };

                if (includeItemsOfTheWeak)
                {
                    items = items.Where(m => m.IsPieOfTheWeek == true);
                }

                var mappedItems = _mapper.Map<IEnumerable<ItemDTO>>(items);
                return Ok(mappedItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(int id)
        {
            try
            {
                var item = await _itemRepository.GetItemByIdAsync(id);

                if (item == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<ItemDTO>(item));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> Create(ItemCreatingDTO itemCreatingDto)
        {
            try
            {
                var category = await _categoryRepository.FindAsync(itemCreatingDto.CategoryId);

                if (category == null)
                {
                    return BadRequest($"Category with ID {itemCreatingDto.CategoryId} not found.");
                }

                var item = _mapper.Map<Item>(itemCreatingDto);

                item.CategoryId = itemCreatingDto.CategoryId;
                item.Category = category;

                if (!TryValidateModel(item))
                {
                    return BadRequest(ModelState);
                }

                await _itemRepository.CreateAsync(item);

                var createdItemDto = _mapper.Map<ItemDTO>(item);

                return CreatedAtAction(nameof(GetItem), new { id = createdItemDto.Id }, createdItemDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDTO>> Update(ItemUpdatingDTO itemUpdatingDto, int id)
        {
            try
            {
                if (! await _itemRepository.ExistsAsync(id))
                {
                    return NotFound();
                }

                var category = await _categoryRepository.FindAsync(itemUpdatingDto.CategoryId);

                if (category == null)
                {
                    return BadRequest(new { categoryId = new { message = $"Category with ID {itemUpdatingDto.CategoryId} not found."}
                    });
                }

                if (!await _itemRepository.ExistsAsync(id))
                    return NotFound();
                var itemEntity = await _itemRepository.GetItemByIdAsync(id);
                if (itemEntity == null)
                    return NotFound();

                itemEntity.CategoryId = itemUpdatingDto.CategoryId;
                itemEntity.Category = category;

                _mapper.Map(itemUpdatingDto, itemEntity);


                if (!TryValidateModel(itemEntity))
                {
                    return BadRequest(ModelState);
                }

                await _itemRepository.SaveChangesAsync();

                var createdItemDto = _mapper.Map<ItemDTO>(itemEntity);

                return CreatedAtAction(nameof(GetItem), new { id = createdItemDto.Id }, createdItemDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (!await _itemRepository.ExistsAsync(id))
                {
                    return NotFound();
                }

                await _itemRepository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
