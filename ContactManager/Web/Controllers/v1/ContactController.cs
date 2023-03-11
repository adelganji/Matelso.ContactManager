using Application.Commands.Contacts.CreateContact;
using Application.Commands.Contacts.UpdateContact;
using Application.Commands.Contacts.DeleteContact;
using Application.Queries.Contacts.GetAllContacts;
using Application.Queries.Contacts.GetContact;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Contract.v1;
using Domain.Primitives.Result;

namespace Web.Controllers.v1;

public class ContactController : ApiControllerBase
{
    public ContactController(IMediator mediator) : base(mediator)
    {
    }


    /// <summary>
    /// Reading one contact entry by a given id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>list of </returns>
    /// <remarks>
    /// Sample query:
    ///
    /// /api/v1/contact/1
    ///
    /// </remarks>
    /// <response code="200">Returns contact from a given id</response>
    /// <response code="404">If the item not found</response>
    /// <response code="400">If any error occurs</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet(ApiRoutes.Contact.GetContactById)]
    public async Task<IActionResult> GetContact(int Id)
    {
        try
        {
            var res = await QueryAsync(new GetContactByIdQuery { ContactId = Id });
            if(res.Succeed && res.HasValue)
                return StatusCode(StatusCodes.Status200OK, res);
            else
                return StatusCode(StatusCodes.Status404NotFound, res);

        }
        catch (Exception ex)
        {
            return await CheckException(ex);
        }
    }
    /// <summary>
    /// Get all contacts
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Returns all contacts data </response>
    /// <response code="400">If any error occurs </response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet(ApiRoutes.Contact.GetContacts)]
    public async Task<IActionResult> GetAllContacts()
    {
        try
        {
            var res = await QueryAsync(new GetAllContactsQuery());
            return Ok(res);
        }
        catch (Exception ex)
        {
            return await CheckException(ex);
        }
    }

    /// <summary>
    /// Create a Contact
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Todo
    ///     {
    ///       "salution": "Mr",
    ///       "firstName": "Adel",
    ///       "lastName": "P. Ganji",
    ///       "displayName": "",
    ///       "birthdate": "1990-02-25",
    ///       "email": "Adelganji@gmail.com",
    ///       "phoneNumber": "+989113131524"
    ///     }    
    /// </remarks>
    /// <response code="201">Returns created contact Id</response>
    /// <response code="400">If any error occurs</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost(ApiRoutes.Contact.CreateContact)]
    public async Task<IActionResult> CreateContact([FromBody] CreateContactCommand command)
    {
        try
        {
            var res = await CommandAsync(command);
            if (res.Succeed)
                return StatusCode(StatusCodes.Status201Created, res);
            else
                return StatusCode(StatusCodes.Status400BadRequest, res);
        }
        catch (Exception ex)
        {
            return await CheckException(ex);
        }
    }

    /// <summary>
    /// Update a contact 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /Todo
    ///     {
    ///       "Id": 1,
    ///       "salution": "Mr",
    ///       "firstName": "Adel",
    ///       "lastName": "P. Ganji",
    ///       "displayName": "Adel Pourramezan Ganji",
    ///       "birthdate": "1990-02-25",
    ///       "email": "Adelganji@gmail.com",
    ///       "phoneNumber": "+989113131524"
    ///     }    
    /// </remarks>
    /// <response code="200">Returns if contact has updated</response>
    /// <response code="404">If the item not found</response>
    /// <response code="400">If any error occurs</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut(ApiRoutes.Contact.UpdateContact)]
    public async Task<IActionResult> UpdateContact([FromBody]UpdateContactCommand command)
    {
        try
        {
            var res = await CommandAsync(command);
            if (res.Succeed )
                return StatusCode(StatusCodes.Status200OK, res);
            else
                return StatusCode(StatusCodes.Status404NotFound, res);
        }
        catch (Exception ex)
        {
            return await CheckException(ex);
        }
    }

    /// <summary>
    /// Delete a specific contact entry by a given Id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns>true of false</returns>
    /// <remarks>
    /// Sample query:
    ///
    /// /api/v1/contact/1
    ///
    /// </remarks>
    /// <response code="200">Returns if contact has deleted</response>
    /// <response code="404">If the item not found</response>
    /// <response code="400">If any error occurs</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete(ApiRoutes.Contact.DeleteContact)]
    public async Task<IActionResult> DeleteContact(int Id)
    {
        try
        {
            var res = await CommandAsync(new DeleteContactCommand { Id= Id });
            if(res.Value)
                return StatusCode(StatusCodes.Status200OK, res);
            else
                return StatusCode(StatusCodes.Status404NotFound, res);
        }
        catch (Exception ex)
        {
            return await CheckException(ex);
        }
    }

}
