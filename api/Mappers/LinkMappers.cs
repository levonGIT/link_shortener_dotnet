using api.Dtos.Link;
using api.Models;

namespace api.Mappers
{
    public static class LinkMappers
    {
        public static LinkDto ToLinkDto(this Link linkModel)
        {
            return new LinkDto
            {
                Id = linkModel.Id,
                OriginLink = linkModel.OriginLink,
                ShortLink = linkModel.ShortLink,
                VisitCount = linkModel.VisitCount,
                CreatedOn = linkModel.CreatedOn,
            };
        }

        public static Link ToLinkFromCreateDTO(this CreateLinkRequestDto linkDto)
        {
            return new Link
            {
                OriginLink = linkDto.OriginLink,
                VisitCount = linkDto.VisitCount,
                CreatedOn = linkDto.CreatedOn,
            };
        }
    }
}
