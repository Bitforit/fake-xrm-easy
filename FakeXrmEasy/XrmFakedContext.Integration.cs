using FakeItEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeXrmEasy
{
    public partial class XrmFakedContext
    {
        protected internal IOrganizationService _integrationService { get; set; }

        protected internal bool UsesIntegration
        {
            get
            {
                return _integrationService != null;
            }
        }

        /// <summary>
        /// This method will sync all the entities in the context that were initialised using the Initialize method,
        /// by creating them in the organization service passed as a parameter.
        /// </summary>
        /// <param name="realService">A Real organization service reference where the entities will be created automatically</param>
        protected void Sync(IOrganizationService realService)
        {

        }

        /// <summary>
        /// Similar to Sync, it will delete all the entity records created by the Sync method adn other entities created by each test
        /// It will basically delete any entity in the context
        /// </summary>
        /// <param name="realService"></param>
        protected void CleanUp(IOrganizationService realService)
        {

        }

        /// <summary>
        /// Fakes the Execute method of the organization service.
        /// Not all the OrganizationRequest are going to be implemented, so stay tunned on updates!
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fakedService"></param>
        public static void FakeExecuteIntegration(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.Execute(A<OrganizationRequest>._))
                .ReturnsLazily((OrganizationRequest req) =>
                {
                    return context._integrationService.Execute(req);
                });
        }

        public static void FakeRetrieveMultipleIntegration(XrmFakedContext context, IOrganizationService fakedService)
        {
            A.CallTo(() => fakedService.RetrieveMultiple(A<QueryBase>._))
                .ReturnsLazily((QueryBase req) =>
                {
                    return context._integrationService.RetrieveMultiple(req);
                });
        }

    }

}