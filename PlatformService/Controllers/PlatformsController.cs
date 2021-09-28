using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataService.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController:ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandClient;

        public PlatformsController(IPlatformRepo repository,IMapper mapper, ICommandDataClient commandDataClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandClient = commandDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            var platforms = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable< Platform>>(platforms));
        }

        [HttpGet("{id}",Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var plt = _repository.GetPlatformById(id);
            if (plt != null)
            {
                return Ok(_mapper.Map<Platform>(plt));
            }
            else
                return NotFound();
        }

        [HttpPost]
        public async Task< ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformDto)
        {
            var platform = _mapper.Map<Platform>(platformDto);
            _repository.CreatePlatform(platform);
            _repository.SaveChanges();

            var platReadDto = _mapper.Map<PlatformReadDto>(platform);
            try
            {
                await _commandClient.SendPlatformToCommand(platReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            return CreatedAtRoute(nameof(GetPlatformById), new { id = platform.Id }, _mapper.Map<PlatformReadDto>(platform));
        }
    }
}
