using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MistralRestaurant.API.Data;
using MistralRestaurant.API.Dtos.RecipeCategory;
using MistralRestaurant.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MistralRestaurant.API.Services.RecipeCategoryService
{
    public class RecipeCategoryService : IRecipeCategoryService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecipeCategoryService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetRecipeCategoryDto>>> GetAllRecipeCategoriesServices(int page, int numberItems)
        {
            var serviceResponse = new ServiceResponse<List<GetRecipeCategoryDto>>();

            try
            {
                int maxId = page * numberItems;
                int maxIdFromDB = _context.RecipeCategories.Max(r => r.Id);

                if ((page > 0 && numberItems > 0) && maxIdFromDB > ((page - 1) * numberItems))
                {
                    if (maxIdFromDB <= maxId)
                    {
                        maxId = maxIdFromDB;
                    }
                }
                else
                {
                    serviceResponse.Success = false;
                    if (page > 0 && numberItems > 0)
                    { 
                        serviceResponse.Message = "List is empty.";
                    } 
                    else
                    {
                        serviceResponse.Message = "Incorrect input parameters.";
                    }

                    return serviceResponse;
                }

                var dbRecipeCategories = await _context.RecipeCategories.Where(r => r.Id > (page - 1) * numberItems && r.Id <= maxId).ToListAsync();

                serviceResponse.Data = dbRecipeCategories.Select(c => _mapper.Map<GetRecipeCategoryDto>(c)).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                throw;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRecipeCategoryDto>> GetRecipeCategoryServices(int id)
        {
            var serviceResponse = new ServiceResponse<GetRecipeCategoryDto>();

            try
            {
                int maxIdFromDB = _context.RecipeCategories.Max(r => r.Id);

                if (id > 0 && maxIdFromDB >= id)
                {
                    var dbRecipeCategories = await _context.RecipeCategories.FirstAsync(r => r.Id == id);

                    serviceResponse.Data = _mapper.Map<GetRecipeCategoryDto>(dbRecipeCategories);
                }
                else
                {
                    serviceResponse.Success = false;
                    if (id > 0)
                    {
                        serviceResponse.Message = "Recipe not found.";
                    }
                    else
                    {
                        serviceResponse.Message = "Incorrect input parameters.";
                    }

                    return serviceResponse;
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                throw;
            }

            return serviceResponse;
        }
    }
}
