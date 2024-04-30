using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using ReportPortal.Client.Abstractions.Models;
using ReportPortal.Client.Abstractions.Requests;
using ReportPortal.Client.Abstractions.Responses;
using ReportPortal.E2E.API.Business;
using ReportPortal.E2E.API.Business.Models.Responses;
using ReportPortal.E2E.API.Business.Models.Responses.Items;
using ReportPortal.E2E.API.Business.StepDefinitions;
using ReportPortal.E2E.API.Tests.Scenarios.NunitTest.BaseTest;
using ReportPortal.E2E.Core.Helpers;

namespace ReportPortal.E2E.API.Tests.Scenarios
{
    public class DemoLaunchesTests : BaseNunitTest
    {
        private new static readonly string ProjectName = $"AQA-Project-{RandomValuesHelper.RandomString(5)}";
        private static List<LaunchItem> _projectLaunches;

        protected override void Preconditions()
        {
            DemoLaunchesTestsStepDefinitions.GivenNewProjectCreatedWithDemoLaunches(ProjectName);
            _projectLaunches = Steps.AsAdminUser().GetLaunchesByFilter<GetLaunchesResponse>(ProjectName).Launches;
        }


        [Test]
        public void Get_Demo_Launches_Name()
        {
            var names = Steps.AsAdminUser().GetLaunchNames<List<string>>(ProjectName);

            names.Should().HaveCount(1);
            names!.Single().Should().Be("Demo Api Tests");
        }

        [Test]
        public void Get_Non_Existing_Launch_By_Id_Returns_Error()
        {
            var responseBody = Steps.AsAdminUser().GetLaunchById<ErrorResponse>(ProjectName, int.MaxValue);

            using (new AssertionScope())
            {
                responseBody.ErrorCode.Should().Be(4041);
                responseBody.Message.Should().Be($"Launch '{int.MaxValue}' not found. Did you use correct Launch ID?");
            }
        }

        [Test]
        public void Post_Create_Launch_Cluster()
        {
            var launchId = _projectLaunches.First().Id;
            var createClusterMessage = Steps.AsAdminUser().PostLaunchCluster<SuccessfulMessageResponse>(ProjectName, launchId.ToString()).Message;

            using (new AssertionScope())
            {
                createClusterMessage.Should().Be($"Clusters generation for launch with ID='{launchId}' started.");
            }
        }

        [Test]
        public void Post_Create_Launch_Cluster_With_Null_Id_Value_Returns_Error()
        {
            var responseBody = Steps.AsAdminUser().PostLaunchCluster<ErrorResponse>(ProjectName, null);

            using (new AssertionScope())
            {
                responseBody.ErrorCode.Should().Be(4001);
                responseBody.Message.Should().Be("Incorrect Request. [Field 'launchId' should not be null.] ");
            }
        }

        [Test]
        public void Post_Create_Launch_Cluster_With_Non_Existing_Id_Returns_Error()
        {
            var responseBody = Steps.AsAdminUser().PostLaunchCluster<ErrorResponse>(ProjectName, int.MaxValue.ToString());

            using (new AssertionScope())
            {
                responseBody.ErrorCode.Should().Be(4041);
                responseBody.Message.Should().Be($"Launch '{int.MaxValue}' not found. Did you use correct Launch ID?");
            }
        }

        [Test]
        public void Put_Launch_Update()
        {
            var launchId = _projectLaunches.First().Id;
            var requestBody = new UpdateLaunchRequest
            {
                Description = RandomValuesHelper.RandomString(30),
                Mode = LaunchMode.Default,
                Attributes = new List<ItemAttribute>
                {
                    new()
                    {
                        Key = RandomValuesHelper.RandomString(5),
                        Value = RandomValuesHelper.RandomString(5)
                    }
                }
            };

            var updateMessageBody = Steps.AsAdminUser().PutLaunchUpdate<SuccessfulMessageResponse>(ProjectName, launchId, requestBody);
            var getActualLaunchInfo = Steps.AsAdminUser().GetLaunchById<LaunchResponse>(ProjectName, launchId);

            using (new AssertionScope())
            {
                updateMessageBody.Message.Should().Be($"Launch with ID = '{launchId}' successfully updated.");
                getActualLaunchInfo.Description.Should().Be(requestBody.Description);
                getActualLaunchInfo.Mode.Should().Be(LaunchMode.Default);
                getActualLaunchInfo.Attributes.Single().Key.Should().Be(requestBody.Attributes.Single().Key);
                getActualLaunchInfo.Attributes.Single().Value.Should().Be(requestBody.Attributes.Single().Value);
            };
        }

        [Test]
        public void Put_Launch_Update_With_Empty_Attribute_Value_Returns_Error()
        {
            var launchId = _projectLaunches.First().Id;
            var requestBody = new UpdateLaunchRequest
            {
                Description = RandomValuesHelper.RandomString(30),
                Mode = LaunchMode.Default,
                Attributes = new List<ItemAttribute>
                {
                    new()
                    {
                        Key = RandomValuesHelper.RandomString(5),
                        Value = null
                    }
                }
            };

            var updateMessageBody = Steps.AsAdminUser().PutLaunchUpdate<ErrorResponse>(ProjectName, launchId, requestBody);

            using (new AssertionScope())
            {
                updateMessageBody.ErrorCode.Should().Be(4001);
                updateMessageBody.Message.Should().Be("Incorrect Request. [Field 'attributes[].value' should not" +
                                                      " contain only white spaces and shouldn't be empty.] ");
            };
        }

        [Test]
        public void Put_Launch_Update_With_Non_Existing_Launch_Id_Returns_Error()
        {
            var updateMessageBody = Steps.AsAdminUser().PutLaunchUpdate<ErrorResponse>(ProjectName, int.MaxValue, new UpdateLaunchRequest());

            using (new AssertionScope())
            {
                updateMessageBody.ErrorCode.Should().Be(4041);
                updateMessageBody.Message.Should().Be($"Launch '{int.MaxValue}' not found. Did you use correct Launch ID?");
            };
        }

        [Test]
        public void Patch_Launch_Description_Update()
        {
            var launchId = _projectLaunches.First().Id;
            var description = RandomValuesHelper.RandomString(100);
            var requestBody = new UpdateLaunchRequest { Description = description };

            var expectedLaunchInfo = Steps.AsAdminUser().GetLaunchById<LaunchResponse>(ProjectName, launchId);
            var updateLaunchDescription = Steps.AsAdminUser()
                .PutLaunchUpdate<SuccessfulMessageResponse>(ProjectName, launchId, requestBody);
            var actualLaunchInfo = Steps.AsAdminUser().GetLaunchById<LaunchResponse>(ProjectName, launchId);

            using (new AssertionScope())
            {
                updateLaunchDescription.Message.Should().Be($"Launch with ID = '{launchId}' successfully updated.");
                actualLaunchInfo.Description.Should().Be(description);
                actualLaunchInfo.Name.Should().Be(expectedLaunchInfo.Name);
                actualLaunchInfo.Mode.Should().Be(expectedLaunchInfo.Mode);
                actualLaunchInfo.Attributes.Should().BeEquivalentTo(expectedLaunchInfo.Attributes);
            }
        }

        [Test]
        public void Patch_Launch_Attribute_With_Null_Value_Returns_Error()
        {
            var launchId = _projectLaunches.First().Id;
            var requestBody = new UpdateLaunchRequest { Attributes = new List<ItemAttribute>
            {
                new()
                {
                    Key = RandomValuesHelper.RandomString(),
                    Value = null
                }
            }};
            var updateLaunchResponse = Steps.AsAdminUser()
                .PutLaunchUpdate<ErrorResponse>(ProjectName, launchId, requestBody);

            using (new AssertionScope())
            {
                updateLaunchResponse.ErrorCode.Should().Be(4001);
                updateLaunchResponse.Message.Should().Be("Incorrect Request. [Field 'attributes[].value' should not contain only white spaces and shouldn't be empty.] ");
            }
        }

        [Test]
        public void Patch_Launch_Attribute_With_WhiteSpace_Value_Returns_Error()
        {
            var launchId = _projectLaunches.First().Id;
            var requestBody = new UpdateLaunchRequest
            {
                Attributes = new List<ItemAttribute>
                {
                    new()
                    {
                        Key = RandomValuesHelper.RandomString(),
                        Value = " "
                    }
                }
            };
            var updateLaunchResponse = Steps.AsAdminUser()
                .PutLaunchUpdate<ErrorResponse>(ProjectName, launchId, requestBody);

            using (new AssertionScope())
            {
                updateLaunchResponse.ErrorCode.Should().Be(4001);
                updateLaunchResponse.Message.Should().Be("Incorrect Request. [Field 'attributes[].value' should not contain only white spaces and shouldn't be empty.] ");
            }
        }

        [Test]
        public void Delete_Launch_By_Id()
        {
            var launchId = _projectLaunches.Last().Id;
            var deleteResponseBody = Steps.AsAdminUser().DeleteLaunchById<SuccessfulMessageResponse>(ProjectName, launchId);

            deleteResponseBody.Message.Should().Be($"Launch with ID = '{launchId}' successfully deleted.");
        }

        [Test]
        public void Delete_Non_Existing_Launch_Returns_Error()
        {
            var updateMessageBody = Steps.AsAdminUser().DeleteLaunchById<ErrorResponse>(ProjectName, int.MaxValue);

            using (new AssertionScope())
            {
                updateMessageBody.ErrorCode.Should().Be(4041);
                updateMessageBody.Message.Should().Be($"Launch '{int.MaxValue}' not found. Did you use correct Launch ID?");
            };
        }
    }
}
