﻿using api.Data;
using api.Dtos.Link;
using api.Mappers;
using api.Models;
using api.Repository;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/links")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly LinkRepository _linkRepo;

        public LinksController(ApplicationDBContext context, LinkRepository linkRepo)
        {
            _context = context;
            _linkRepo = linkRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLinkRequestDto linkDto)
        {

            var linkModel = linkDto.ToLinkFromCreateDTO();
            string code = LinkShortenerService.RandomString();
            while (await _linkRepo.GetByCodeAsync(code) != null)
            {
                code = LinkShortenerService.RandomString();
            }
            linkModel.Code = code;
            /* Можно было не сохранять короткую ссылку в базу данных, а склеивать ее при выдаче,
             * т.к. в какой то момент может смениться хост, а база остаться прежней.
             * База данных уже спроектирована, поэтому я оставил всё как есть
            */
            linkModel.ShortLink = $"{this.Request.Scheme}://{this.Request.Host}/{code}";
            // TODO: Здесь необходима проверка авторизации и добавление автора ссылки при наличии

            await _linkRepo.CreateAsync(linkModel);
            return Ok(linkModel.ToLinkDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var links = await _linkRepo.GetAllAsync();
            var linksDto = links.Select(l => l.ToLinkDto()).ToList();
            return Ok(linksDto);
        }

        [HttpGet]
        [Route("/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            var linkModel = await _linkRepo.GetByCodeAsync(code);
            if (linkModel!=null)
            {
                await _linkRepo.IncreaseVisitCounter(linkModel);
                return Redirect(linkModel.OriginLink);
            }
            return BadRequest();
        }
    }
}
