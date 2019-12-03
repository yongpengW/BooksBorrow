using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksBorrow.Middlewares
{
    public class BooksBorrowAuthorizationPolicyProvider: IAuthorizationPolicyProvider
    {
        private AuthorizationOptions _options;
        public BooksBorrowAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _options = options.Value;
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return Task.FromResult(_options.DefaultPolicy);
        }

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            //因为我们policy的名称其实就是对应的权限名称，所以可以用下列逻辑返回需要的验证规则
            AuthorizationPolicy policy = _options.GetPolicy(policyName);
            if (policy == null)
            {
                string[] resourceValues = policyName.Split(new char[] { '-' }, StringSplitOptions.None);
                if (resourceValues.Length == 1)
                {
                    _options.AddPolicy(policyName, builder =>
                    {
                        builder.AddRequirements(new ClaimsAuthorizationRequirement(resourceValues[0], null));
                    });
                }
                else
                {
                    _options.AddPolicy(policyName, builder =>
                    {
                        builder.AddRequirements(new ClaimsAuthorizationRequirement(resourceValues[0], new string[] { resourceValues[1] }));
                    });
                }
            }
            return Task.FromResult(_options.GetPolicy(policyName));
        }
    }
}
