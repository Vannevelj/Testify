# Testify

This project contains the core logic of setting up the code for experimentation.

There are, codewise, two main parts to an experiment:

1. Are we in the variant?
2. Should we record a participation?

This project provides the answer to the former and the infrastructure to record the answer to the latter.

It exposes the following, crucial method:

```
Task<bool> IsInVariantAsync(int experimentId, int userId, bool recordParticipation = true);
```

Some observations:

1. The experiment ID is an int, not an enum. This allows us to re-use this package across different multiverses without having to maintain a core enum and update multiple packages each time. In the package-specific implementation we can have an `enum` where we use more userfriendly names.
All of this goes paired with a central way of storing the experiments: the database. We don't attempt to maintain a list in sync, everything just goes in the database with an autoincrement key.

2. The user ID is core to every interaction. This does require that a user exists and thus we are excluding unauthenticated users from experimentation for now. Through the user ID we can find out everything else (e.g. team ID, role, etc) so this is the perfect common basis differentiator for now.

3. `recordParticipation` should always be true. The only time it shouldn't be, is when we don't have the knowledge yet serverside. For more info, see the `Testify.WebApp` project.

4. It is `async` because there will be database interaction. Caching would be  helpful here since it's likely that you will be using the same experiment and users more than once. Don't make the cache too long though so we can turn experiments off inside the database (or provide a cache reset mechanism).

## What about a base experiment class? DI? Experiment runners?
I would suggest to hold off on adding a lot of infrastructure around writing the code for experiments. Let's do a bunch of very basic implementations first (`if invariant then x else y`) and see what the painpoints are after a while of using this so we can properly address the complexities.

## Where does the experiment logic go: clientside or serverside?
I strongly prefer serverside logic which is why I didn't add an endpoint to call `IsInVariant` from javascript (that would just be one more extra request for the server to handle when it already knows all the data beforehand). What I suggest instead is we do the logic in the backend (which can very well be right inside the controller's action method), store the result of our `IsInVariant` calls somewhere in the request and then conditionally output bits of code in the view.

Example:

```
class VideoController
{
  public Task<IActionResult> Watch(int videoId)
  {
    if (today == DayOfWeek.Monday &&
        user.Country == "Belgium" &&
        _experimentService.IsInVariant(experimentId: 20, userId: user.Id, recordParticipation: true))
        {
          Request.Properties["experiment_20_invariant]= true;
          Request.Properties["pay_button_color"] = "#fff";
        }
  }
}
```

In the view you then use these elements to conditionally change the color of the pay button on Mondays for Belgian users.
This can be cleaned up a lot by adding some helpers around the dictionary manipulation but you get the gist of it.

**note**: pay attention to how I've first checked every prerequisite before calling `IsInVariant` (and thus recording the participation). That way we avoid recording participations for non-Belgians and non-Mondays. If I had put the `IsInVariant` call first then our results would be heavily skewed.
