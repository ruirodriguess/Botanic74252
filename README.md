<p align="center">
  <img src="https://static.vecteezy.com/ti/vetor-gratis/p1/9644381-banner-plante-e-deixe-espaco-para-texto-em-fundo-branco-vetor.jpg" width="100%" height="200" title="hover text">
</p>

<h1 align="center">Welcome to Botanic74252 :shamrock:</h1>
<h6 align="center">This is a web application developed as part of a programming course project. The Botanic WebApp focuses on botany-related activities such as purchasing products, daily challenges, and a blog section</h3>
<br />

> :green_square: Access the web application through your preferred web browser at [HERE](https://ruirumos74252.azurewebsites.net/)
<br />

### Technologies Used
* ASP.NET MVC (C#)
* HTML/CSS
* JavaScript
* Python
* Entity Framework (for data access)
* Unit Testing
* GitHub (for version control and project repository)
* Azure
* DevOps

<details>
  <summary>MY PIPELINE</summary>

  ```
  trigger:
- master

pool:
  vmImage: 'windows-latest'

variables:
  solution: '**/RuiRumos74252.sln'
  unittests: '**/TestProject/TestProject.csproj'
  buildPlatform: 'Any CPU'
  buildConfiguration: 'Release'
  azureSubscription: '0c279d29-0c31-44bc-8797-7839cfae9ca8'
  appName: 'RuiRumos74252'
  artifactName: 'articFactNameRui'

steps:
- task: NuGetToolInstaller@1

- task: NuGetCommand@2
  inputs:
    restoreSolution: '$(solution)'
- task: DotNetCoreCLI@2
  inputs:
    command: 'restore'
    projects: '**/RuiRumos74252.sln'
    feedsToUse: 'select'
    vstsFeed: '3357f51f-0f3a-40fe-8816-ec45ae9c8862'

- task: DotNetCoreCLI@2
  inputs:
    command: 'build'
    projects: '$(solution)'

- task: DotNetCoreCLI@2
  inputs:
    command: 'test'
    projects: '$(unittests)'

- task: DotNetCoreCLI@2
  inputs:
    command: 'publish'
    projects: '**/RuiRumos74252/RuiRumos74252.csproj'
    publishWebProjects: true
    arguments: '--configuration $(BuildConfiguration)  --output $(Build.ArtifactStagingDirectory)'
    zipAfterPublish: True

- task: PublishBuildArtifacts@1
  displayName: Publish Artifacts
  inputs:
    PathtoPublish: '$(Build.ArtifactStagingDirectory)'
    ArtifactName: '$(artifactName)'

- task: AzureWebApp@1
  inputs:
    azureSubscription: 'ruirodriguesrumos74252(0c279d29-0c31-44bc-8797-7839cfae9ca8)'
    appType: 'webApp'
    appName: 'RuiRumos74252'
    package: '$(System.ArtifactsDirectory)/**/*.zip'
    deploymentMethod: 'auto'
  ```
</details>

### Azure Resources Used
* Storage Account
* App Service
* SQL Server
* SQL Database
* Cosmos DB

  
### Features
* Product Shop: Users can browse and purchase botany-related products
* Daily Challenge: Users can participate in daily challenges where they need to guess the name of an image related to botany
* Blog Section: Users can upload photos to share with others and receive comments on their posts

<br />
<h4 align="center">:bulb: Resume :bulb:</h1>
<br />

The Botany WebApp is a web application developed as a final project for a programming course. The project aims to provide an interactive platform related to botany. The application features three main functionalities.

The first functionality allows users to purchase botany-related products. Users can browse the product catalog, add items to the cart, and proceed with checkout.

The second functionality is the Daily Challenge, where users are presented with an image related to botany and challenged to guess its name. Each day features a new image, enabling users to test their knowledge and track their progress.

The third functionality is the blog section, where users can upload their own botany-related photos and receive comments from other users. This section fosters a community around the shared passion for botany, providing a platform for learning, inspiration, and connection.

The Botany WebApp was developed using the ASP.NET MVC framework in C#, along with front-end technologies such as HTML, CSS, and JavaScript. I tried to create a python application to handle checkout queues but I couldn't finish. The purpose of the application was after the queue, an email was sent to the carrier.

The application was version-controlled on GitHub for versioning and collaboration. The project was also implemented with a scalable and modular architecture, following best practices in web development. A pipeline was created in devops for CI/CD.

This project was ambitious and I went through several difficulties, but I came out better prepared for the real world. Hope you like the final result.

<br />

### This application have:

* Public part (accessible without authentication)
* Private part (available for registered users)
* Administrative part (available for administrators only) - Ask me 

### Prerequisites
* Visual Studio 2022 or higher
* .NET 7.x SDK

<br />

## Contacts 

For any questions, feel free to send an email to ruirodrigues04@outlook.pt

<br />

<h3 align="center">Connect with me:</h3>
<p align="center">
<a href="https://linkedin.com/in/ruirodrigues-dev" target="blank"><img align="center" src="https://raw.githubusercontent.com/rahuldkjain/github-profile-readme-generator/master/src/images/icons/Social/linked-in-alt.svg" alt="ruirodrigues" height="30" width="40" /></a>
<a href="https://instagram.com/ruirodrigues04" target="blank"><img align="center" src="https://raw.githubusercontent.com/rahuldkjain/github-profile-readme-generator/master/src/images/icons/Social/instagram.svg" alt="ruirodrigues04" height="30" width="40" /></a>
<a href="https://discord.gg/ruirodrigues04@outlook.pt" target="blank"><img align="center" src="https://raw.githubusercontent.com/rahuldkjain/github-profile-readme-generator/master/src/images/icons/Social/discord.svg" alt="ruirodrigues04@outlook.pt" height="30" width="40" /></a>
</p>
