using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BrewApp.API.Data;
using BrewApp.API.Dtos;
using BrewApp.API.helpers;
using BrewApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// user controller
namespace BrewApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // set up repo and mapper references
        private readonly IBrewRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IBrewRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // get users from repo and map from dto
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userFromRepo = await _repo.GetUser(currentUserId);
            userParams.User_Id = currentUserId;

            var users = await _repo.GetUsers(userParams);
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            // add pagination
            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            var userToReturn = _mapper.Map<UserForDetailedDto>(user);
            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            var userFromRepo = await _repo.GetUser(id);
            _mapper.Map(userForUpdateDto, userFromRepo);
            if (await _repo.SaveAll())
            {
                return NoContent();
            }
            throw new Exception($"Updating user {id} failed on save");
        }

        [HttpPost("{id}/like/{recipientId}")]
        public async Task<IActionResult> LikeUser(int id, int recipientId)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }
            var like = await _repo.GetLike(id, recipientId);
            if (like != null)
            {
                return BadRequest("User already liked");
            }
            if (await _repo.GetUser(recipientId) == null)
            {
                return NotFound();
            }
            like = new Like 
            {
                LikerId = id,
                LikeeId = recipientId
            };
            _repo.Add<Like>(like);
            if (await _repo.SaveAll())
            {
                return Ok();
            }
            return BadRequest("Failed to like user");
        }
    }
}