# Demonhunter

... is an attempt at creating a CMS based on c# thats scalar und fits the needs of modern webdevelopment. Heavily service-based and self repairing. ("Everything that can go wrong, will go wrong"- Assumption)

Components:

- Najentus
	- First Boss of the Black Temple
	- Therefore the HTML-Listener
	
- Templating System (to be named)
	- Based on a custom grammar
	- uses grammatica
	
- Datasource System (to be named)
	- First iteration will use MongoDB
	- API designed to work with other Data Sources
	

Utility-Classes
- Nathrezim (WIP)
	- A Messaging Protocol that connects all the components together. It uses a master-slave principle that can decide on its master on its own. Self-Repairing if the master goes down. Goal is that you add the .dll, say new Nathrezim(); and then you are ready to go.

- Runebook
	- Simple Logging
