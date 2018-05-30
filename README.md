# Royal Flow Manager

[![Build Status](https://img.shields.io/travis/pshomov/reducto.svg?style=flat-square)](https://travis-ci.org/pshomov/reducto)
[![NuGet Release](https://img.shields.io/nuget/v/Reducto.svg?style=flat-square)](https://www.nuget.org/packages/RoyalFlowManager/)

## What is Royal Flow Manager?

Royal Flow Manager is a infrastructure for managing your mobile app flows. 

|Metric|Value|
|-------|-----|
|Dependencies| 0 |
|Packaging | [NuGet PCL](https://www.nuget.org/packages/RoyalFlowManager/) |

## Installation

In Package Manager Console run

```
PM> Install-Package RoyalFlowManager
```

## Key concepts

 - **Flow** - . 
 - **FlowState** - .
 - **FlowRouter** - . 
 - **State** - . 
 - **FlowManager** - .


## How to use it?

Here is a short example of Royal Flow Manager in action. Let's create an app for registering users. 

First, we need to define the `FlowState`.
Lets name it `SignUpFlowState`:

```c#
// SignUpFlowState

public class SignUpFlowState
{
    // Will appear in the page #1
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    
    // Will appear in the page #2
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    // Will appear in the page #3
    public string Username { get; set; }
    public string Password { get; set; }
}

```
