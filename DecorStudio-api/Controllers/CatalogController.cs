using DecorStudio_api.DTOs;
using DecorStudio_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DecorStudio_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : Controller
    {
        private readonly CatalogService service;
        
        public CatalogController(CatalogService service)
        {
            this.service = service;
        }

        [HttpGet("get-all/{storeId}")]
        public async Task<IActionResult> GetAll(int storeId)
        {
            try
            {
                var list = await service.GetCatalogs(storeId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-id/{storeId}/{catalogId}")]
        public async Task<IActionResult> GetById(int storeId, int catalogId)
        {
            try
            {
                var app = await service.GetCatalog(storeId, catalogId);
                return Ok(app);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //get catalog by id
        [HttpGet("get-catalog-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var app = await service.GetCatalog(id);
                return Ok(app);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-catalog")]
        public async Task<IActionResult> AddCatalog([FromBody] CatalogDto dto)
        {
            try
            {
                var app = await service.CreateCatalog(dto);
                return Ok(app);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCatalog(int id)
        {
            try
            {
                var app = await service.DeleteCatalog(id);
                return Ok(app);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CatalogDto dto)
        {
            try
            {
                var app = await service.UpdateCatalog(id, dto);
                return Ok(app);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-decor")]
        public async Task<IActionResult> AddDecorCatalog([FromBody] Catalog_DecorDto dto)
        {
            try
            {
                var cd = await service.CreateCatalog_Decor(dto);
                return Ok(cd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-from-catalog/{catalogId}/{decorId}")]
        public async Task<IActionResult> DeleteCatalogDecot(int catalogId, int decorId)
        {
            try
            {
                var cd = await service.DeleteCatalog_Decor(catalogId, decorId);
                return Ok(cd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-catalog-decor/{catalogId}")]
        public async Task<IActionResult> GetAllDecors(int catalogId)
        {
            try
            {
                var cd = await service.GetCatalog_Decors(catalogId);
                return Ok(cd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
