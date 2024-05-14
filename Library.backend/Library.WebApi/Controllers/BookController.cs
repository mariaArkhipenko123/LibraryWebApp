using AutoMapper;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Application.Library.Queries.GetLibraryList;
using Library.Application.Library.Queries.GetLibraryDetails;
using Library.Application.Library.Commands.CreateLibrary;
using Library.Application.Library.Commands.UpdateLibrary;
using Library.Application.Library.Commands.DeleteCommand;
using Library.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Library.Persistense.Repository;
using Library.Domain;
using MediatR;

namespace Library.WebApi.Controllers
{
    [ApiVersion("1.0")]
   //[ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/[controller]")]
   
    public class BookController : BaseController
    {
        private readonly IMediator _mediator;

        private readonly IMapper _mapper;

        public BookController( IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
       
        /// <summary>
        /// Gets the list of books
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /book
        /// </remarks>
        /// <returns>Returns LibraryListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LibraryListVm>> GetAll(int pageSize = 10, int pageNumber = 1)
        {
            var query = new GetLibraryListQuery { UserId = UserId, PageSize = pageSize, PageNumber = pageNumber };
            var vm = await _mediator.Send(query);
            return Ok(vm);
        }
        /// <summary>
        /// Gets the book by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /book/D34D349E-43B8-429E-BCA4-793C932FD580
        /// </remarks>
        /// <param name="id">Book id (guid)</param>
        /// <returns>Returns LibraryDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user in unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LibraryDetailsVm>> Get(Guid id)
        {
            var query = new GetLibraryDetailsQuery { Id = id, UserId = UserId };
            var vm = await _mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the book
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /book
        /// {
        ///     title: "book title",
        ///      descriptions: "book descriptions"
        /// }
        /// </remarks>
        /// <param name="createlibraryeDto">CreateLibraryDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateLibraryDto createlibraryDto)
        {
            var command = _mapper.Map<CreateLibraryCommand>(createlibraryDto);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }
        /// <summary>
        /// Updates the book
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /book
        /// {
        ///     title: "updated book title"
        /// }
        /// </remarks>
        /// <param name="updatelibraryDto">UpdateLibraryDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateLibraryDto updatelibraryDto)
        {
            var command = _mapper.Map<UpdateLibraryCommand>(updatelibraryDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Deletes the book by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /book/88DEB432-062F-43DE-8DCD-8B6EF79073D3
        /// </remarks>
        /// <param name="id">Id of the book (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteLibraryCommand { Id = id, UserId = UserId };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}

