using _09_Configuration_Custom;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.Extensions.Configuration
{
    public static class MyConfigurationSourceExtension
    {
        public static IConfigurationBuilder AddSource( this IConfigurationBuilder configurationBuilder)
        {
            MyConfigurationSource myConfigurationSource = new MyConfigurationSource();
            configurationBuilder.Add(myConfigurationSource);
            return configurationBuilder;
        }

        public static IConfigurationBuilder AddCustomSource(this IConfigurationBuilder configurationBuilder)
        {
            CustomConfigurationSource customConfigurationSource = new CustomConfigurationSource();
            configurationBuilder.Add(customConfigurationSource);
            return configurationBuilder;
        }
    }
}
