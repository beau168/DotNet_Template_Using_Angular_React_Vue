using CoreApi.Application.Auth.Commands.CreateUser;
using CoreApi.Application.Auth.Queries.GetUserByEmail;
using CoreApi.Application.Auth.Queries.ValidateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users");

        group.MapPost("/", async (CreateUserCommand command, ISender sender) =>
        {
            try
            {
                var userId = await sender.Send(command);
                return Results.Created($"/api/users/{userId}", new { UserId = userId });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Error = ex.Message });
            }
        });

        group.MapPost("/validate", async (ValidateUserQuery query, ISender sender) =>
        {
            var user = await sender.Send(query);
            if (user == null)
            {
                return Results.Unauthorized();
            }
            return Results.Ok(user);
        });

        group.MapGet("/by-email/{email}", async (string email, ISender sender) =>
        {
            var user = await sender.Send(new GetUserByEmailQuery(email));
            if (user == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(user);
        });
    }
}
