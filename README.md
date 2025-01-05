# Contact List App

A simple .NET application for managing contacts, built as part of my C# course within my .NET Web Developer education at EC Utbildning Helsingborg 2024-2026. This app allows users to store, view, and manage their contacts.

## Features
Anything marked with a + has been implemented
### For "Godkänt" (Passing Grade)
[+] A **console application** with the following functionalities:
  + **List all contacts**: Displays a list of all saved contacts.
  + **Create a new contact**: Add a new contact with the following fields:
    - First Name
    - Last Name
    - Email Address
    - Phone Number
    - Street Address
    - Postal Code
    - City
  + Each contact is assigned a **unique ID** using a GUID.
  + Contacts are **saved to a `.json` file**.
  + The application **loads contacts from a `.json` file**:
    + On startup
    + Whenever the contact list is updated.
  + **Applies the "S" principle from SOLID** in code structure.
  + **Unit tests** (without mock) for methods that can be tested without mocking.

### For "Väl Godkänt" (High Distinction)
- Includes all features from the "Godkänt" level and adds the following:
  - A **GUI application** with:
    - A page to **list all contacts**.
    - A page to **create a new contact**.
    - A page to **edit a contact**, including options to update or delete the contact.
  + **Dependency Injection** for better modularity and testing.
  + Use of **Class Libraries** to organize code.
  + Implementation of multiple **Design Patterns**:
    + SOLID principles
    + Service Pattern
    + Factory Pattern
  + **Unit tests** with mocks (where necessary) for all methods.

## Technologies Used
- Language: C#
- Frameworks: .NET Console and GUI [To be decided]
- File Format: JSON for contact storage
- Testing: xUnit for unit testing
