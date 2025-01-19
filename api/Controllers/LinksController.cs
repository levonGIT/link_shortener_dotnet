using api.Data;
using api.Dtos.Link;
using api.Mappers;
using api.Repository;
using api.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly UserRepository _userRepo;

        public LinksController(ApplicationDBContext context, LinkRepository linkRepo, UserRepository userRepo)
        {
            _context = context;
            _linkRepo = linkRepo;
            _userRepo = userRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLinkRequestDto linkDto)
        {
            if (!Uri.TryCreate(linkDto.OriginLink, UriKind.Absolute, out _))
            {
                return BadRequest("Url is invalid");
            }
            var linkModel = linkDto.ToLinkFromCreateDTO();
            string code = LinkShortenerService.RandomString();
            while (await _linkRepo.GetByCodeAsync(code) != null)
            {
                code = LinkShortenerService.RandomString();
            }
            linkModel.Code = code;
            linkModel.ShortLink = $"{this.Request.Scheme}://{this.Request.Host}/{code}";
            var username = Request.HttpContext.User.Identity?.Name;
            if (username != null)
            {
                var user = await _userRepo.GetByLoginAsync(username);
                linkModel.UserId = user?.Id;
            }
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
