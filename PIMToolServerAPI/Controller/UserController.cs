using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PIMToolServerAPI.Controller;

[ApiController, Route("/api/v1/users")]
[Authorize(Roles = "Admin")]
public class UserController: ControllerBase {

}