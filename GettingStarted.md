# GeometryGymIfc toolkit basic principles

# Installation

GeometryGymIfc is available via Nuget. 
When starting a new project, open the Nuget package manager and search for `GeometryGymIfc`  ([see here](https://www.nuget.org/packages/GeometryGymIFC/))

Installing via package manager console: 

    Install-Package GeometryGymIFC

If a specific version is of interest, add a version flag: 

    Install-Package GeometryGymIFC -Version 0.1.6

Add the reference to your code: 

    using GeometryGym.Ifc;



# Create, open, and save an Ifc file

## Create new Ifc model

    var database = new DatabaseIfc(ModelView.Ifc4X3_RC2); 

When creating a new database, the user can choose if some boilerplate data should be generated automatically. 

    var database = new DatabaseIfc(true, ModelView.Ifc4X3_RC2). 
    
The first argument triggers the boilerplate generation.
If the constructor is called only be the desired ModelView item, the boilerplate is created by default.

## Open an existing Ifc model

    string path = "<filepath/to/model>";
    var database = new DatabaseIfc(path);

## Save model in STEP-P21 

    database.WriteFile("myIfcFile.ifc");

<!-- ## Save model in IFCXML

    to be added soon...
  -->
<!-- 
## Setting units in model

### Definition of specific units

    to be added soon... -->
<!-- 
### Definition of units using the factory

    to be added soon ...  -->

<!-- 
## Class constructors
 

## out statements in constructors

    to be added soon ... -->

## Database factory

Once the `DatabaseIfc` instance is created, it offers various settings 

### Example 1: Set the application developer: 

    database.Factory.ApplicationDeveloper = "Developer Name";

### Example 2: Set the application name: 

    database.Factory.ApplicationFullName = "IFC Application"; 

### Example 3: Turn off the auto generation of an IfcOwnerHistory: 

    database.Factory.Options.GenerateOwnerHistory = false; 

### Example 4: Add a textual comment in the resulting `*.ifc`  file: 

    var element = new IfcBuiltElement(database);
    element.AddComment("some comment");

results in 

    /* some comment */
    #1= IFCBUILTELEMENT('3X4z6TQDjEfweLPwC9InC4',#7,$,$,$,$,$,$);

<!-- ### Example 5: create a fixed guid

Fixing the GUIDs especially is helpful when creating test models. 

    to be added soon ...

## Duplicate factory
usage: copy concepts from one to another IFC model. 

 -->

# General conventions

## Constructors
Constructors for IFC classes are typically require all non-optional attributes. 
Passing `null` into any constructor is typically not intended and leads to non schema compliant IFC files.
