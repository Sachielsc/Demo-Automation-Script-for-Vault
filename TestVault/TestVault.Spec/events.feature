Feature: events
	In order to ensure events are added and editable
	As a user
	I want to be able to add an event via the portal and edit events in the Events register

@mytag
Scenario: Add an Event via the portal
	Given I am on the Event portal page
	And I have selected report an injury
	And I have input all the fields
	When I press save
	Then the result should be represented on the Events register

Scenario: Edit an Event via the Events register
	Given I am on the Event register page
	And I click edit on an event
	And I fill out the mandatory filds
	When I press save
	Then the edited event should be represented on the Events register