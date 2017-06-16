using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ShoppingStore.Data.ViewModels;
using ShoppingStore.Data.ViewModels.AccountViewModels;
using ShoppingStore.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShoppingStore.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/roles")]
    public class RolesController : BaseIdentityController
    {
        //private AppRoleManager roleManager
        //{
        //    get
        //    {
        //        return Request.GetOwinContext().GetUserManager<AppRoleManager>();
        //    }
        //}
        [Route("{id:guid}", Name = "GetRoleById")]
        public async Task<IHttpActionResult> GetRole(string id)
        {


            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(ResponseResult.ShowRole(role));
        }

        [Route("", Name = "GetRoles")]
        public IHttpActionResult GetRoles()
        {
            return Ok(roleManager.Roles);
        }

        public async Task<IHttpActionResult> CreateRole(
            CreateRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = new AppRole
            {
                Name = model.Name
            };

            var result = await roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                Uri locationHeader =
                    new Uri(
                        Url.Link("GetRoleById", new { id = role.Id }));

                return Created(locationHeader, ResponseResult.ShowRole(role));
            }

            return GetErrorResult(result);
        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var result = await roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return Ok();
            }

            return GetErrorResult(result);
        }


    }
}
