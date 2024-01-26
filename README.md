# Dependency-Captivity

	- Hierarchy
		- IReferenceGenerator (Top level service registered with Singleton since we have a counter here which we want to share across different items)
			- Dependency - IDateTimeProvider (Registered as Scoped since we want different datetime for each product)

		- Expected output
			- Expect code to generate a new date-time + Increment the counter
				- 2024-01-26T00:00:00.000-000
				- 2024-01-26T00:00:02.000-001
				- 2024-01-26T00:00:04.000-002
		- Actual Output
				- 2024-01-26T00:00:00.000-000
				- 2024-01-26T00:00:00.000-001
				- 2024-01-26T00:00:00.000-002
		
		- Explanation
			- Since top level statement is singleton, its dependencies will also be INITIATED ONLY ONCE.
			- This makes DateTimeProvider as effectively a Singleton in our case
			- ReferenceGenerator is holding DateTimeProvider CAPTIVE
			- This would mean the dependency acts as a captive for the parent/top level service

		- Container provides way for us to validate if there are any dependency captivity scenario present in our code
			- Use ValidateOnScope = true (Refer Program)
		