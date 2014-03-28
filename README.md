EnergyTrading.MDM.Sample
===========================

This repository contains an example implementation of the EnergyTrading.MDM components.

It consists of:

1. [Code/EntityContracts/] Sample entity / contract definitions
2. [Code/Service/] MDM Service implementation serving the sample entities
3. [Code/ClientApi/] MDM Client Api wrapping the REST interface for the sample entities
4. [Code/AdminUi/] MDM Admin Ui for the maintenance of the sample entities
5. [Code/EntityLoader/] MDM Loader tool for bulk uploading XML data files for the sample entities
6. [Database/] MS-SQL database script to create the required tables etc for the sample entities
7. [Documentation/] Any supporting documentation resides here

Getting Started
================

Running the service
-------------------
* First step is to create the database and run the table creation scripts under the Database/ folder.
* Update the connection string in the web.config under the Code/Service/MDM.ServiceHost.Wcf.Sample/ folder with the correct details.
* The service should now start OK when run within Visual Studio.
* Issue a "http://localhost:<port number>/sourcesystem/list" in a browser, it should return an empty <SourceSystemList/> payload.

Running the Admin UI
--------------------
* Update the app.config under the Code/AdminUi/Admin.Shell/ folder with the correct MDM service URL.  It's the "service_localhost" appSetting value.
* Set both the Admin.Shell and MDM.ServiceHost.Wcf.Sample projects as Start Up Projects.  Specify the service host to start without debugging.
* Running the solution should now see the Admin UI fire up and connecting to the local service.
* Refer to the "MDM Admin Ui.docx" document under the Documentation/ folder for more information on the Ui.

Using the MDM Entity Loader
---------------------------
* Configure the correct MDM service URL in the app.config under the Code/EntityLoader/MDM.Loader/ folder.  It's the "MdmUri" appSetting value.
* Run the project, a charming WinForms UI should pop up
* You select which entity type you wish to load from the MDM Entities drop down list.
* Next specify the source folder where the entity contract XML file resides.  The loader expects a file named "<entity>.xml".
* Press the "Import" button to run the load, you can click on the "Log" tab to verify progress via log statements.
* The "Candidate Data" check box when checked prevents existing entities in the database from being updated.  (Only new mappings will be created)
* The loader always performs a 'Name' match (or MdmId if specified in the mappings) and if found then that entity is updated with the incoming data (if this checkbox is left unchecked).
  
