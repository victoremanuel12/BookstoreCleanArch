using Microsoft.AspNetCore.Authorization;

namespace Identity.PolicyRequirements
{
    public class HorarioComercialHandler : AuthorizationHandler<HorarioComercialRequeriment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HorarioComercialRequeriment requirement)
        {
            var horarioAtual = TimeOnly.FromDateTime(DateTime.Now);
            if (horarioAtual.Hour >= 8 && horarioAtual.Hour <= 18)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
