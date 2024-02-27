# IGaming.Bet

# Setup instruction:
  1) Clone Project
  2) You need your current connection string and database to run api.
  3) Update your database based on current migrations
  4) Run project

# Design decisions and challenges:
  1) I've decide to create layering architecture, so there is presentation, service and repository layers.
  2) I also Used ef core for ORM and Fluent Api.
  3) For betting, I thought That will be better If I would create integration layer for future gaming providers. Service layer has controll for providers layer.
  4) Also I just used simple exception middleware, I think that middleware will be anough for testing purposes, so i didn't add other middlewares like: audit logging, or rate limiting.
  
# Other
  1) There is 4 endpoints just like in exercise.
  2) I tried to do all the requirements, so you can check it. (fill free for any questions)
  
 
