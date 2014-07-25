EkzoPluginsSystem
=================

ASP.NET MVC plugins system based on precompiled views, generatad by <a href="http://visualstudiogallery.msdn.microsoft.com/1f6ec6ff-e89b-4c47-8e79-d2d68df894ec">RazorGenerator tool</a>.
You need manualy install RazorGenerator in Visual Studion to enable precompiled view generating.

Description
=================
This project is implementation of plugin architecture based website. 
Main project contains functions to load modules, view precompiled views and control web application lifecycle.
Each plugin is independent website(web application), so you can develope it without adding core site to solution.

Post build events
-----------------
Post build events is powerful tool to control project. In this project post build events create "build" directory in solution folder and copy files from core website and plugins.


What's next?
-----------------
<ol>
<li>Next part of work is widgets.</li>
<li>Create online plugin manager to make work comfortable.</li>
<li>Clean code and update lightweight branch.</li>
</ol>

Current project TODOs
------------------

- [ ] Add comments to code. More comments needed
- [ ] Update pluign manager to not lock plugins files
- [ ] Add deafults
  - [ ] Default interfaces
  - [ ] Default providers
- [ ] Add dependency injection

Global TODOs
-----------------
- [ ] Create full functional CMS based on this project
