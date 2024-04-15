Feature: UsersDemoLaunchesTests

Assigned users with different roles are able to view launches


Scenario Outline: Demo launches available for all roles members
	Given I am logged in as '<user>'
	When I am on Launch page of 'default-project'
	And I am switch to 'All' launches view
	Then '<LauncesCount>' launches are displayed
Examples:
	| user					| LauncesCount |
	| aqausercustomer		| 5            |
	| aqauseroperator		| 5            |
	| aqausermember			| 5            |
	| aqauserprojectmanager | 5            |


Scenario Outline: Latest Demo launch available for all roles members
	Given I am logged in as '<user>'
	When I am on Launch page of 'default-project'
	And I am switch to 'Latest' launches view
	Then '1' launches are displayed
	And Launches contains execution information
		| Total | Passed | Failed | Skipped |
		| 30    | 30     | 0      | 0       |
Examples:
	| user					|
	| aqausercustomer		|
	| aqauseroperator		|
	| aqausermember			|
	| aqauserprojectmanager |
