Feature: Clock

@mytag

  Scenario: Set an alarm for Monday, Tuesday, Wednesday, Thursday, and Friday with the name Trumpf Metamation
    Given I open the Windows Clock app
    When I set the alarm for 9:00 AM on Monday, Tuesday, Wednesday, Thursday, and Friday with the name "Trumpf Metamation"
    Then the alarm should be set for 9:00 AM on Monday, Tuesday, Wednesday, Thursday, and Friday with the name "Trumpf Metamation"


  Scenario: Set an alarm for a specific time on Monday
    Given I open the Windows Clock app
    When I set the alarm for 7:00 AM on Monday
    Then the alarm should be set for 7:00 AM on Monday

  Scenario: Set an alarm for a specific time on Wednesday
    Given I open the Windows Clock app
    When I set the alarm for 8:00 AM on Wednesday
    Then the alarm should be set for 8:00 AM on Wednesday

    
  Scenario: Delete the alarm named Trumpf Metamation
    Given I open the Windows Clock app
    When I delete the alarm named "Trumpf Metamation"
    Then the alarm named "Trumpf Metamation" should no longer exist