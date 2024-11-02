using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PIMToolServerAPI.Controller;

[ApiController, Route("/api/v1/employees"), Authorize]
public class EmployeeController: ControllerBase {

}