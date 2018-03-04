# Testify.WebApp

This is a very thin wrapper around `Testify` which exposes a method to record participations for an experiment when we can only determine that clientside.

An example would be a feature that relies on browser support (like the Share API) which we cannot determine server-side.

How many of these experiments we will end up doing remains to be seen. In my experience these are the minority of cases but considering we have a very front-end heavy code base I figured it would be worthwhile to include the notion of it at least.
It is definitely possible to postpone this part.
