Feature: DemoLaunchesTests

As user I want to be able to create new project with demo launches

Background:
	Given I am logged in as SuperAdmin
	When I am on Launch page of 'default-project'
	

Scenario: Demo launches count
	When I am switch to 'All' launches view
	Then '5' launches are displayed
	And Launches contains execution information
		| Total | Passed | Failed | Skipped |
		| 30    | 30     | 0      | 0       |
		| 25    | 20     | 5      | 0       |
		| 20    | 10     | 8      | 2       |
		| 15    | 5      | 9      | 1       |
		| 10    | 1      | 9      | 0       |

Scenario: Demo latest launch
	When I am switch to 'Latest' launches view
	Then '1' launches are displayed
	And Launches contains execution information
		| Total | Passed | Failed | Skipped |
		| 30    | 30     | 0      | 0       |

