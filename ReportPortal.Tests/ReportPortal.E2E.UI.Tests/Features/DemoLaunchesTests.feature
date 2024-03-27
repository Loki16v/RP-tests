Feature: DemoLaunchesTests

As user I want to be able to create new project with demo launches

#Dummy test
Scenario: Demo launches count
	Given New project 'demo-project' created with demo launches
	And I am logged in as SuperAdmin
	When I am on Launch page of 'demo-project'
	Then '5' launches are displayed