# MunicipalityApp

## Table of Contents

1. [Introduction](#introduction)
2. [Prerequisites](#prerequisites)
3. [Installation](#installation)
4. [Running the Application](#running-the-application)
5. [Usage](#usage)
6. [Features](#features)
7. [Troubleshooting](#troubleshooting)
8. [Contact](#contact)

9. ## Introduction

**MunicipalityApp** is a WPF application designed to provide municipal services in a user-friendly manner. It features a responsive user interface that adjusts properly when resized to different screen sizes, including fullscreen mode.

## Prerequisites

Before compiling and running the application, ensure you have the following:

- **.NET SDK** (version 6.0 or later)
- **Visual Studio** (Community, Professional, or Enterprise edition) with the following components:
  - .NET Desktop Development workload
  - Windows Presentation Foundation (WPF) tools
- **Git** (optional, for version control)
- **Windows OS** (Windows 10 or later)

## Installation

### 1. Clone the Repository

If using Git:
https://github.com/VCDN-2024/prog7312-part-1-DylanFlashman/

Or download the project as a .zip file and extract it.

### 2. Open the Solution in Visual Studio
Navigate to the folder where you cloned/extracted the repository.
Open the MunicipalityApp.sln file in Visual Studio.

### 3. Restore NuGet Packages
Once the solution is opened, restore NuGet packages by either:

Clicking on Tools > NuGet Package Manager > Restore NuGet Packages in Visual Studio, or
Building the project (NuGet will automatically restore during the build).

## Running the Application

### 1. Build the Solution
Go to Build > Build Solution or press Ctrl + Shift + B.
Ensure the build completes successfully without errors.

### 2. Run the Application
Click Debug > Start Without Debugging (or press Ctrl + F5) to run the app.
The application window should open, showing the main interface of the MunicipalityApp.

## Usage

### Main Features:
### 1. UI Resizing:

The application adjusts its layout dynamically when resized, maintaining structure and usability on different screen sizes and fullscreen.
### 2. Municipal Services:

Users can interact with different municipal services through the app, such as submitting service requests, providing feedback on the application.

### Navigating the App:
Use the top menu to access different sections (e.g., Home, Services, Feedback, etc.).

## Features:
### 1. Responsive Design: Maintains the application's structure when resizing.
### 2. Service Requests: Users can submit and track service requests.
### 3. Feedback Reports: Users can provide feedback based on the app, and provide insight into the areas that need improvement.

## Troubleshooting
### 1. Build Errors
Issue: Unable to build the solution.
Solution: Ensure you have installed the correct .NET SDK and WPF tools for Visual Studio.
Issue: Missing NuGet packages.
Solution: Try restoring the packages by going to Tools > NuGet Package Manager > Restore NuGet Packages.

### 2. Application Crashes
Issue: Application crashes on startup.
Solution: Check if all project dependencies are properly installed. Ensure the WPF application targets the correct .NET version (6.0 or higher).

### 3. UI Issues
Issue: The layout is distorted when resizing.
Solution: Ensure the resolution is within recommended dimensions, or report any unusual behavior to the development team.

## Contact
If you encounter any issues or have questions, please contact the development team:

Email: st10083498@vcconnect.edu.za
GitHub Issues: Submit an Issue
