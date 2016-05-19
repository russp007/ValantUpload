# ValantUpload
Patrick Russell, 05/18/2016
This is my solution for Valant's coding exercise.

#Instructions for running solution:
1.  Launch Visual Studio 2015 and load solution.
2.  Rebuild solution and launch in debug (F5).
3.  Use a client of your choosing (such as Chrome's Advanced Rest Client) to connect. 
    NOTE: Don't forget, if running locally, to include the port # in your client.
4.  Alternatively, from the main menu select Test => Run => All Tests.

#Developer's Notes
The architecture here is pretty much standard, there's not a lot of (intentional) deviation.

I left the 'Home' and 'Values' controllers intact, and the default home page, as I'm concerned 
with the API and not the UI for this exercise.

My original intent was to Moq the unit tests.  However, this seemed (to me) to be overkill given
the complexity of the dependencies in this solution so I saved myself the time.

For expiration I require an external invocation on the inventory manager's ProcessExpirations() method.
This is basically just a hook for some type of scheduled job to trigger.  It's not really this service's
responsibility to care about when to fire off such a task, it just needs to be able to respond to the message.

IoC supplied by Unity.

I did not implement the async/await pattern on the controller methods for this example.  Even in a 
production environment that might be overkill for an app with such low latency.  I did, however, use
locks to manage concurrency issues on the repository.

The rationale behind NotificationWriter was simply to provide a wrapper between the consumer and the
implementor.  This actually proved useful in my testing, as it let me create an implementation for
testing that gives access to the notification content.

#Assumptions

1.  An item's 'Label' property is unique and will be used as the PK.
2.  An external process (i.e. batch job) will be responsible for triggering the check for expiration.
3.  Attempt to add duplicate item shall fail gracefully.
4.  Attempt to remove non-existing item shall fail gracefully.

In closing, I hope you enjoy this humble solution.  It's time for a code review.

Regards;
Patrick
