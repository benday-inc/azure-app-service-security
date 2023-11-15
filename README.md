# Azure App Services & App Service Security with ASP.NET Core: Source Code

Written by Benjamin Day  
Pluralsight Author | Microsoft MVP | Scrum.org Professional Scrum Trainer  
https://www.benday.com  
info@benday.com 

This is the source code for the samples in my Azure App Services & App Service Security with ASP.NET Core video course. 
The course is being published on my [YouTube channel](https://www.youtube.com/channel/UCV3ijeRGEvCq_Q5H1Kxy4RA). [Here is the 
official playlist for the course.](https://www.youtube.com/playlist?list=PLGxFXI4dC2sj5ImCCbOb-sjfiMXKS86MF)

## Course Overview

Azure App Services help you to quickly and easily deploy web applications to Azure. When you host your web app using Azure App Services, you get scalability and reliability without having to worry about pesky day-to-day details like hardware, software, operating system patches, electricity, networking, etc. In this course, you'll learn how to create an Azure App Service in order to host your ASP.NET Core applications. We'll also spend lots of time covering Azure App Service Authentication and Authorization -- aka. "easy auth" -- from the perspective of an ASP.NET Core developer. We'll start with the simple security scenarios and then quickly move into how to integrate with and extend Azure App Service security using ASP.NET Core.

Course Duration: 1 hour 15 minutes


## Chapter 1: Create & Deploy an ASP.NET Core Web App to an Azure App Service

This chapter introduces you to Azure App Services and provides an overview for the course. We focus on understanding how Azure App Services scale using App Service Plans and then move on to deploying a basic ASP.NET Core application to the cloud.

Duration: 17 minutes

Clips in this chapter: 

- Introduction
- What is an Azure App Service?
- What is an App Service Plan?
- Demo: Create an Azure App Service
- Demo: Deploy an ASP.NET Core App using Visual Studio Code
- Demo: Deploy an ASP.NET Core App using Visual Studio 2019

## Chapter 2: Azure App Service Security

This chapter covers the basics of Azure App Service authorization and authentication and also provides a quick overview of security in ASP.NET Core.

Duration: 15 minutes

Clips in this chapter:

- Demo: Enable App Service Security
- Demo: Understanding App Service Security: Headers & /.auth/me
- ASP.NET Core Security Overview
- ASP.NET Core Authorization: Roles, Claims, Requirements, and Handlers

## Chapter 3: Integrating & Extending App Service Security with ASP.NET Core

This chapter goes deep into the structure of Azure App Service security and ASP.NET Core Security. We focus on understanding how Azure App Service security works and how you can use custom middleware to integrate with ASP.NET Core security. We also cover how to implement ASP.NET Core authorization policies and authorization handlers.

Duration: 42 minutes

Clips in this chapter: 
- App Service Auth Headers, Claims, and ASP.NET Core Middleware
- Demo: Use Middleware to Populate User Claims from App Service Security
- Complex Scenarios with Azure App Service Security
- How Does Azure App Service Security Actually Work?
- Demo: Implement & Configure Multiple Authentication Providers
- Middleware & Claims, Part 1 of 2: Call /.auth/me from Middleware to Get More User Information
- Middleware & Claims, Part 2 of 2: Use Middleware to Populate Custom Claims from a Database
- Demo: Implement Development Mode Security in ASP.NET Core
- Demo: Implement an ASP.NET Core Authorization Policy Handler
- Summary and Thanks