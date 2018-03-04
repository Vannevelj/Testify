# Core tenets of experimentation
## Why do we AB test?
AB testing is about improving existing processes and introducing new features in a data-driven manner. It allows you to make decisions based on actual data rather than relying on empirical sources like coaches or support staff. It also helps you avoid spending time on building out features that in reality have little effect on business metrics.

## Make the experiment as fail-fast as possible
If you have an idea in mind that would require a lot of work to implement, consider whether it's worthwhile to first verify interest in this specific feature.

Say, for example, you decide to add Facebook as a login provider. Rather than overhauling your authentication system and adding significant debt (once added, it's very hard to remove auth methods) you could instead decide to add a facebook button first, show a "not yet supported" message when they click and track how often it's clicked. Based upon that data, you can decide whether to implement it or not.

The same idea goes for features that do have an actual implementation for the "trial" steps: if you want to change the way the entire library page looks then you split it up in smaller chunks and see how they perform.

## Make the experiment as small as possible
By splitting an experiment in small chunks you reach several goals

1. Quick feedback loop through more frequent deploys and thus faster gathering of data
2. Ability to steer direction on subsequent iterations
3. Smaller risk of introducing defects due to less code changes
4. Isolated results to analyse what specific changes were essential

For example: rather than implementing one big experiment to change the library page, you would have separate experiments to

* Show a banner with the team's picture
* Change from a Grid to a List layout
* Show a recent activity ticker
* Add a button to each video with an explicit 'Watch' CTA
* Change the "pay now" button to say "continue using hudl"

A major benefit of following this approach is that you don't discard small gains: an implementation composed of multiple aspects might have some positive and some negative changes included. If you instead split it up into minimal viable changes, you can keep the positives and discard the negatives. Not only will that teach you what works and what doesn't (and thus improve future features) but it also means that you are continuously improving rather than resetting back to the start after each failed big feature.

## Use a handful of evaluation types
One risk of going down the rabbit hole is adding a new set of goals for every specific experiment. A goal for a bet might be set by the business to increase video impressions as well as increase participation of those videos, likely with a specific target for each of the criteria. Rather than creating a new evaluation type that has these specific metrics, you should aim to stick with a few core metrics that you, as a business, care about and improve those.

For example: you want to change the way users can sign up to your platform and you will evaluate it based on the number of videos that have at least 10 impressions and 2 interactions. Rather than creating a new custom evaluation type that represents this, you evaluate based on a handful of carefully chosen evaluation types:

* Video impressions
* Video interactions
* Sales contacts through website
* Support requests via help page
* Succesful upload rate

The most important part is that you believe each of these evaluation metrics has a strong correlation with eventual revenue. Some things are directly related (fewer support requests => fewer support staff required) while others are merely an indication (does an additional video impression actually increase revenue?). It is up to you to figure out what they are.

## Don't quantify evaluations
An experiment's result is affected by many different factors and should never be taken as an absolute value. There is no such thing as "this experiment increased sign up rate by 10%". While that is a reasonable way to describe it, it is important to keep in mind that the experiment only runs for a certain amount of time and there is great variaton in the population that uses it.

Some common factors that affect an experiment:
* Time: does the experiment run during football season or basketball? Do both sports react equally?
* Geography: does it work well in the USA but not in Germany? Is it a cultural difference?
* Language: are the Spanish translations on par with the default English?
* Experience: is the feature intuitive enough for users that are new and might not know the lingo?
* Level: is it something that has more attraction in an elite environment rather than a competitive one?
* Future: if you have all of the above sorted out and it works great -- will it still hold true when you include curling in a decade? Or when you expand to Zimbabwe?

Because of these factors (and many more) you should strive to find a trend, not a specific number.
Similarly, when you calculate the significance of an experiment then you should include the requirement that it was significant for a period of time. If not, you risk basing ourselves on data that is within the error margins of insignificance and then you might as well do a coin flip.

In the end, if you combine the concept of "small experiments" with "embrace iterations", you will receive an end result that is close or more than what you originally aimed for. You can still do a point-in-time comparison between the bet's start and the bet's end on the specific measurement criteria but it will be implemented through many iteratively small features.

## Embrace iterations
Iterations are the magic of AB testing: they allow you to capitalise on the knowledge you gained from the previous experiment.

### Handling positive results
Positive results are self explanatory: they already added value by definition but they also teach you what works. It is important that you then take this result and think about how you can expand this further. In my experience, positive results have a higher chance of yielding more positive results so this makes it even more important that you can properly figure out what the positive changes are exactly.

### Handling negative results
Negative results also provide a learning opportunity: they tell you that the users *really* didn't like it. What was your hypothesis for this test? If you gave a 5% discount and you had a significantly negative gross profit result, should you perhaps make it 5% more expensive instead?

This is where analysis tools and introspection of the experiment's hypothesis are invaluable to figuring out next steps. User interviews would help but are time consuming and only represent a fraction of users -- likely a non-representative subset.

### Analysing results
Analysing the outcomes of experiments is paramount to proper AB testing. One thing in particular that you should aim to look for is a common theme between results. Experiments have hypotheses behind them and it is these hypotheses you try to prove. Can you group these hypotheses in a common idea behind them? e.g. are you trying to increase urgency? Are you making a page more intuitive to use? Finding corrolations between these hypotheses and looking at their success rate for certain pages will give you additional data to consider when thinking about next iterations as well as prioritizing those.

## Why no multivariate?
Multivariate tests (with two or more variants as opposed to just one) come with advantages and disadvantages.
The advantage is that you can try out multiple variations at once which guarantees an equal spread of users and all their characteristics for each of the variations. This makes sure you don't test variant A during the soccer season while Variant D is tested during ice hockey prime time.
The disadvantage, however, is that you will need more time to gain the same degree of confidence in your results (because users are now more thinly spread out over variants) which in turn delays iterations. Another important disadvantage is that you open yourself up to willy nilly features: rather than having a solid hypothesis why the button should be red, you now create a multivariate test that tries out red, green, blue and pink.
When you have 10 variants and a 10% false positive rate, you gained no confidence that the chosen variant is actually good.

If you need a multivariate test, the first thing you should ask yourself is what you are trying to accomplish and come up with a more targetted hypothesis.

In my experience, I have used exactly two multivariate tests in a total of approximately 7000 experiments.

1. An attempt at changing the price for a product but without any idea of which direction would provide the biggest improvement. You then create a test that has a control group, a variant group with a 5$ surcharge, a variant with a 10$ surcharge and one with a 5$ discount. By looking at trends you might then be able to formulate a path forward for future price changes.

2. A compliance request that requires you to make a negatively impactful change within a very short period of time. There are a lot of places on the website where you could add it but some of them will have a bigger impact on sales than others -- and all of them are legally valid. Due to time constraints, you could do a multivariate test that at least gives some sort of indication of which ones might be much worse than others.

# Technical considerations
## The database
As outlined [here](https://sync.hudl.com/pages/viewpage.action?pageId=60098407) there are a few considerations involved with the database. As I am unfamiliar with the current database structure I will approach it from an idealistic point of view -- adaptions can be made to accomodate this or we could just find another way of doing it if it's implemented differently.

1. If we want to support experiments for unauthenticated users, introduce a concept like ASP.NET's [`AnonymousID`](https://msdn.microsoft.com/en-us/library/system.web.httprequest.anonymousid(v=vs.110).aspx). This would allow us to provide a unique identifier (differentiator in code) which we use to see if an experiment should be turned on for a user.
2. If we focus on just logged in users, we could have a table structure like the following

```
create table Participations
(
  int UserId,
  datetime2 ParticipationDateTime,
  int ExperimentId,
  int Variant
)

create table Goals
(
  int UserId,
  datetime2 GoalDateTime,
  int GoalType
)
```

The goals would be implemented separate from the experiment: a one-time implementation that sends a rabbit request whenever someone watches a video, contacts support or does any other of our evaluation metrics.

The data might already exist elsewhere but there is value in having it all in a single format as we can then write very generic analysis against it. Alternatively we provide specific implementations for each goal.

Another key aspect of keeping goals and experiments unrelated from eachother is that we can evaluate the experiment on different goals. By using the `UserId` as the primary key we also have the opportunity to provide more granular analysis based on the specific user's features (e.g. is it a coach or a player? Belgian or American?). Depending on how the rest of the database looks, we can also connect this to metadata around their browsing experience such as the client application used (Web? iPad? Android?), what browser (did this feature do bad on IE?), etc.

3. Avoid putting experiment implementation logic in the database: it makes iterating on a feature a magnitude more complex. Ideally, there should be absolutely no interaction between the developer and the database after they create the experiment. Having the implementation entirely in the code gives much more flexibility on the logic you want to add and it's a lot clearer to have all logic in one place.
There are other reasons like the lack of source control as well as proper code reviews around DB data that make it more likely that an issue gets introduced if you take this approach.

## The code
The code included in this repo is a barebones suggestion on what the eventual result could look like. It would have to be adapted to fit the Hudl patterns but these should be approximately the required building blocks. I would encourage to start with a very elementary solution at first and adapt it as needed to avoid overcomplicating something that should be fairly simplistic.

### Testify
The core logic for setting up the code for experimentation.
This project is meant to be used by other multiverses and will be responsible for telling you which variant your user is in and recording server-side participations.

### Testify.WebApp
This provides an endpoint to record client-side participations.

### Testify.Analysis
This provides the analysis tools to verify whether your variant has fulfilled the intended goal as well as maintenance of experiment data in the database.

### Testify.Analysis.WebApp
This provides the user interface towards experiment analysis and maintenance.
