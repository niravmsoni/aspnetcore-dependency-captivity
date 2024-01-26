# Dependency-Captivity

	- Hierarchy
		- IReferenceGenerator (Top level service registered with Singleton since we have a counter here which we want to share across different items)
			- Dependency - IDateTimeProvider (Registered as Scoped since we want different datetime for each product)

		- Output - Dependency Captivity
			- Since top level statement is singleton, its dependencies will also be INITIATED ONLY ONCE.
			- This makes DateTimeProvider as effectively a Singleton in our case
			- ReferenceGenerator is holding DateTimeProvider CAPTIVE
			- This would mean the dependency acts as a captive for the parent/top level service

		- Container provides way for us to validate if there are any dependency captivity scenario present in our code?
		