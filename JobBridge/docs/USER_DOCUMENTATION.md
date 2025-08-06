# JobBridge USER DOCUMENTATION

**Version 1.0 | August 2025**  
**Team 06 - CSE325 Team Project**  
**Term 4 - 2025**
**Brigham Young University - Idaho**

---

## TABLE OF CONTENTS

1. [Introduction](#introduction)
2. [Getting Started](#getting-started)
3. [System Requirements](#ii-system-requirements)
4. [Installation Guide](#installation-guide)
5. [User Roles and Access](#user-roles-and-access)
6. [Feature Guide](#feature-guide)
7. [User Workflow](#user-workflow)
8. [Project Structure](#project-structure)
9. [Design System](#design-system)
10. [Troubleshooting](#troubleshooting)
11. [API Documentation](#api-documentation)
12. [Testing](#testing)
13. [Deployment](#deployment)
14. [Support and Resources](#support-and-resources)

---

## INTRODUCTION

### I. About the Project

**JobBridge** is a modern, comprehensive job matching platform designed to bridge the gap between job seekers and employers in today's dynamic employment landscape. This web application serves as a centralized hub where job seekers can discover opportunities, showcase their profiles, and connect with potential employers, while businesses can efficiently post openings and manage their recruitment processes.

### II. Project Vision

JobBridge aims to solve the common problem of fragmented job searching and hiring processes by providing a streamlined, user-friendly platform that caters to diverse employment needs, from entry-level positions and internships to professional roles and remote opportunities.

### III. Key Features

- **For Job Seekers**: Profile management, advanced job search with filters, bookmark system, application tracking
- **For Employers**: Complete job posting management (CRUD operations), applicant tracking, comprehensive analytics dashboard
- **For Administrators**: User management, content moderation, platform-wide analytics
- **Responsive Design**: Fully optimized for desktop, tablet, and mobile devices
- **Secure Authentication**: Role-based access control using ASP.NET Core Identity
- **Modern UI**: Glassmorphism effects, gradient designs, smooth animations

### IV. Technology Stack

- **Framework**: Blazor Server with ASP.NET Core
- **Database**: SQLite with Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Styling**: Custom CSS with Inter font family, Bootstrap utilities
- **Deployment Target**: Azure App Service

---

## GETTING STARTED

This guide will help to set up and start using JobBridge on the local machine for development and testing purposes.

### I. Prerequisites

Before begin, ensure to have the following installed:

- Git for version control
- .NET 9.0 SDK or later
- Visual Studio 2022 or Visual Studio Code
- A modern web browser (Chrome, Firefox, Edge, Safari)

## II. System Requirements

### Minimum Hardware Requirements

- **Processor**: 1.8 GHz or faster processor
- **RAM**: 4 GB minimum (8 GB recommended)
- **Storage**: 500 MB free space for application and database
- **Display**: 1280x720 resolution minimum

### III. Software Requirements

- **Operating System**:
  - Windows 10 version 1903 or higher
  - macOS 10.15 or higher
  - Ubuntu 20.04 or higher
- **.NET Runtime**: .NET 9.0
- **Database**: SQLite (included with project)
- **Web Browser**: Latest version of Chrome, Firefox, Edge, or Safari

### IV. Development Requirements

- **IDE**: Visual Studio 2022 (recommended) or Visual Studio Code
- **Version Control**: Git 2.0 or higher
- **Azure CLI** (optional, for deployment)

---

## INSTALLATION GUIDE

### Step 1: Clone the Repository

## Clone from Azure DevOps

git clone [https://dev.azure.com/atorekiTEAM/CSE325TeamProject/_git/CSE325TeamProject]

## Navigate to the project directory

cd CSE325TeamProject/JobBridge

### Step 2: Verify Installation

## Check .NET version

dotnet --version
<!-- NOTE: Should show 9.0.x or higher -->

## Check Git version

git --version

### Step 3: Restore Dependencies

## Restore NuGet packages

dotnet restore

### Step 4: Build the Application

## Build the project

dotnet build

<!-- 
NOTE: If successful, should see:
Build succeeded.
0 Warning(s)
0 Error(s)
-->

### Step 5: Database Setup

The SQLite database (`jobbridge.db`) is included with the project. If it needed to be to recreated:

## Install Entity Framework tools
<!-- NOTE: if not already installed-->
dotnet tool install --global dotnet-ef

## Apply database migrations

dotnet ef database update

### Step 6: Run the Application

## Start the application

dotnet run

The application will start and display:
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: <http://localhost:5028/>
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: <http://localhost:5028/>
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.

### Step 7: Access the Application

Open the web browser and navigate to the HTTPS URL shown in the console <!-- typically `https://localhost:5028` or similar) -->

---

## USER ROLES AND ACCESS

JobBridge implements three distinct user roles with specific permissions and access levels:

### 1. JOB SEEKER ROLE

**Purpose**: Individual users looking for employment opportunities

**Access Permissions**:

- Create and maintain personal profile
- Search and filter job listings
- View detailed job information
- Bookmark jobs for later reference
- Track application clicks
- Access personal dashboard

**Restricted From**:

- Creating job postings
- Accessing employer analytics
- Viewing other users' profiles
- Administrative functions

### 2. EMPLOYER ROLE

**Purpose**: Companies and organizations posting job opportunities

**Access Permissions**:

- Create and maintain company profile
- Post new job listings
- Edit existing job postings
- Delete job postings
- Toggle job status (active/inactive)
- View comprehensive analytics dashboard
- Track job performance metrics
- Access applicant engagement data

**Restricted From**:

- Applying to jobs as a job seeker
- Accessing admin controls
- Modifying other employers' postings

### 3. ADMINISTRATOR ROLE

**Purpose**: Platform administrators managing the system

**Access Permissions**:

- Full access to all system features
- User account management
- Content moderation and approval
- Platform-wide analytics
- System configuration
- Database management
- Quality control monitoring

**Special Capabilities**:

- Override user permissions
- Delete inappropriate content
- Ban/suspend accounts
- Generate system reports

---

## FEATURE GUIDE

### JOB SEARCH AND DISCOVERY

#### Search Bar

- **Location**: Homepage and Job Listings page
- **Capabilities**:
  - Keyword search (job titles, skills, companies)
  - Search history (stored locally)

#### Filter Panel

- **Experience Level Options**:
  - Entry-level
  - Internship
  - Mid-level
  - Professional/Senior
  
- **Employment Type**:
  - Full-time
  - Part-time
  - Contract
  - Remote
  - Internship
  
- **Salary Range**:
  - Customizable min/max values
  - Currency selection
  
- **Industry Categories**:
  - Technology
  - Healthcare
  - Finance
  - Education
  - Other industries

#### Job Cards Display

Each job card shows:

- Job title
- Company name
- Location
- Employment type
- Salary range (if provided)
- Posted date
- Application deadline
- Quick bookmark button

---

### JOB POSTING MANAGEMENT

#### CREATING a New Job Post

**Required Fields**:

- Job Title (max 100 characters)
- Company Name
- Job Description (rich text editor)
- Location (city, state/country)
- Employment Type (dropdown)
- Application Deadline (date picker)

**Optional Fields**:

- Salary Range (min/max)
- Experience Required (years)
- Skills Required (tag selection)
- Benefits Package
- External Application URL

#### MANAGING Existing Posts

**Edit Function**:

- All fields remain editable except posting date
- Changes are tracked with timestamps
- Draft saving available

**Delete Function**:

- Confirmation required
- Soft delete (archived, not permanently removed)
- Can be restored within 30 days

**Status Toggle**:

- Active: Visible in search results
- Inactive: Hidden but retained in system
- Expired: Automatically set after deadline

---

### DASHBOARD COMPONENTS

#### I. Job Seeker Dashboard (`add link later`)

**Statistics Card**:

- Total jobs viewed
- Applications clicked
- Profile views (if implemented)
- Bookmarks count

**Recent Activity**:

- Last 5 viewed jobs
- Recent searches
- Bookmark updates

**Recommendations**:

- Jobs matching profile
- Similar to bookmarked jobs

#### II. Employer Dashboard (`add link later`)

**Performance Metrics**:

- Total job views
- Application clicks
- Conversion rate (views to clicks)
- Active vs. inactive postings

**Job Management Table**:

- Job title
- Status
- Views
- Applications
- Days remaining
- Quick actions (edit, toggle, delete)

#### III. Admin Dashboard (`add link later`)

**System Statistics**:

- Active job postings
- Daily activity metrics
- System health status

**Moderation Queue**:

- Flagged content
- New employer verifications
- User reports

### IV. Bookmark System

**Features**:

- One-click bookmark from job cards
- Organized bookmark list
- Filter bookmarks by:
  - Date added
  - Job type
  - Location
  - Status (active/expired)
- Export bookmarks (CSV format)

---

## USER WORKFLOW

### I. JOB SEEKER JOURNEY

#### 1. Account Creation

- Email
- Password (min 8 chars, 1 uppercase, 1 number)
- First Name
- Last Name
- Phone (optional)
→ Email Verification → Profile Setup

#### 2. Profile Completion

- Professional Summary
- Skills
- Experience Level
- Preferred Job Types
- Location Preferences

#### 3. Job Search Process

- "Apply Now" → Redirect to company site
- "Bookmark" → Save for later
- "Back" → Continue searching

#### 4. Application Tracking

- Job Title
- Company
- Date Applied
- Status

### II. EMPLOYER JOURNEY

#### 1. Company Registration

- Company Name
- Industry
- Company Size
- Contact Email
- Website

#### 2. Posting a Job

  Step 1: Basic Information
    - Job Title
    - Department
    - Location
  
  Step 2: Job Details
    - Description
    - Requirements
    - Responsibilities
  
  Step 3: Application Settings
    - Deadline
    - Application Method
    - Contact Information

#### 3. Managing Applications

Dashboard → Select Job → "View Analytics":

- Total Views Graph
- Click-through Rate
- Source of Traffic
- Peak Viewing Times

### III. ADMINISTRATOR JOURNEY

#### 1. Daily Monitoring

- New User Registrations
- Content Flags
- System Alerts

#### 2. Content Moderation

- View Content
- Check User History
- Decision: Approve/Reject/Request Changes

---

## PROJECT STRUCTURE

### Directory Layout

JobBridge/
├── Components/                                              # Blazor components
│   ├── Account/                                            # Identity endpoints
│   │   └── IdentityEndpointsExtensions.cs                  # Identity endpoint extensions
│   ├── Common/                                             # Shared/reusable components
│   │   ├── FilterPanel.razor                               # Job filtering interface
│   │   ├── FilterPanel.razor.css                           # Filter panel styles
│   │   ├── JobCard.razor                                   # Job listing card component
│   │   ├── JobCard.razor.css                               # Job card styles
│   │   ├── Pagination.razor                                # Results pagination
│   │   ├── SearchBar.razor                                 # Search interface
│   │   ├── SearchBar.razor.css                             # Search bar styles
│   │   └── StatisticsCard.razor                            # Dashboard statistics display
│   ├── Layout/                                             # Layout components
│   │   ├── AuthLayout.razor                                # Authentication pages layout
│   │   ├── Footer.razor                                    # Site footer
│   │   ├── Footer.razor.css                                # Footer styles
│   │   ├── MainLayout.razor                                # Main application layout
│   │   ├── MainLayout.razor.css                            # Main layout styles
│   │   ├── NavMenu.razor                                   # Navigation menu
│   │   └── NavMenu.razor.css                               # Nav menu styles
│   ├── Pages/                                              # Application pages
│   │   ├── Admin/                                          # Admin pages
│   │   │   ├── Dashboard.razor                             # Admin dashboard
│   │   │   └── Dashboard.razor.css                         # Admin dashboard styles
│   │   ├── Authentication/                                 # Authentication pages
│   │   │   ├── Login.razor                                 # User login page
│   │   │   ├── Logout.razor                                # Logout handler
│   │   │   └── Register.razor                              # User registration
│   │   ├── Dashboards/                                     # Dashboard pages
│   │   │   ├── EmployerDashboard.razor                     # Employer dashboard page
│   │   │   ├── EmployerDashboard.razor.css                 # Employer dashboard styles
│   │   │   ├── JobSeekerDashboard.razor                    # Job seeker dashboard page
│   │   │   └── JobSeekerDashboard.razor.css                # Job seeker dashboard styles
│   │   ├── Employer/                                       # Employer pages
│   │   │   ├── JobCreate.razor                             # Create new job posting
│   │   │   ├── JobCreate.razor.css                         # Job create styles
│   │   │   ├── JobEdit.razor                               # Edit existing job
│   │   │   ├── JobEdit.razor.css                           # Job edit styles
│   │   │   ├── JobManagement.razor                         # Manage all postings
│   │   │   └── JobManagement.razor.css                     # Job management styles
│   │   ├── Jobs/                                           # Job pages
│   │   │   ├── JobDetails.razor                            # Detailed job view
│   │   │   ├── JobDetails.razor.css                        # Job details styles
│   │   │   ├── JobListings.razor                           # Job search results
│   │   │   └── JobListings.razor.css                       # Job listings styles
│   │   ├── User/                                           # User pages
│   │   │   ├── Profile.razor                               # User profile page
│   │   │   └── Settings.razor                              # User settings
│   │   └── Home.razor                                      # Landing page
│   ├── App.razor                                           # Root component
│   ├── Routes.razor                                        # Routing configuration
│   └── _Imports.razor                                      # Global imports
│
├── Controllers/                                             # API Controllers
│   ├── EmployersController.cs                              # Employer endpoints
│   ├── JobPostController.cs                                # Job posting endpoints
│   └── UsersController.cs                                  # User management endpoints
│
├── Data/                                                    # Database layer
│   ├── JobBridgeContext.cs                                 # EF Core database context
│   └── SeedData.cs                                         # Database seeding
│
├── Identity/                                                # Identity management
│   ├── IdentityRedirectManager.cs                          # Identity redirect handler
│   ├── IdentityUserAccessor.cs                             # User accessor service
│   └── PersistingRevalidatingAuthenticationStateProvider.cs # Auth state provider
│
├── Models/                                                  # Data models
│   ├── ApplicationUser.cs                                  # Extended identity user
│   ├── Bookmarks.cs                                        # Job bookmarks
│   ├── Employer.cs                                         # Employer entity
│   ├── Fields.cs                                           # Job fields/categories
│   ├── JobListing.cs                                       # Job listing model
│   ├── JobPost.cs                                          # Job posting entity
│   └── User.cs                                             # User entity
│
├── Migrations/                                              # EF Core migrations
│   ├── [timestamp]_jobFields.cs                            # Initial fields migration
│   ├── [timestamp]_AddJobPostColumns.cs                    # Add job columns migration
│   ├── [timestamp]_AddedRemainingFieldsToJobPosting.cs     # Complete job fields migration
│   ├── [timestamp]_MakeExternalApplicationUrlNullable.cs   # URL nullable migration
│   ├── [timestamp]_updatedJobPostFields.cs                 # Update job fields migration
│   ├── [timestamp]_applicationDeadlineRequired.cs          # Deadline requirement migration
│   └── JobBridgeContextModelSnapshot.cs                    # Current model snapshot
│
├── Services/                                                # Business logic layer
│   ├── ApplicationService.cs                               # Application management
│   ├── AuthService.cs                                      # Authentication service
│   ├── JobService.cs                                       # Job-related operations
│   └── UserService.cs                                      # User management service
│
├── Properties/                                              # Project properties
│   └── launchSettings.json                                 # Development server config
│
├── wwwroot/                                                 # Static files
│   ├── css/                                                # Stylesheets
│   │   └── app.css                                         # Main stylesheet
│   ├── js/                                                 # JavaScript files
│   │   └── interop.js                                      # JavaScript interop
│   ├── lib/                                                # Third-party libraries
│   │   └── bootstrap/                                      # Bootstrap framework
│   └── favicon.png                                         # Site favicon
│
├── appsettings.json                                        # Production configuration
├── appsettings.Development.json                            # Development configuration
├── JobBridge.csproj                                        # Project file
├── jobbridge.db                                            # SQLite database
└── Program.cs                                              # Application entry point

### Key Components Explanation

**Services Layer**:

- `ApplicationService.cs`: Handles job applications and tracking
- `AuthService.cs`: Manages authentication and authorization
- `JobService.cs`: CRUD operations for job postings
- `UserService.cs`: User profile and preference management

**Data Layer**:

- `JobBridgeContext.cs`: Entity Framework database context
- `SeedData.cs`: Initial data for testing and development

**Identity Layer**:

- Custom identity components for Blazor Server authentication
- Role-based authorization implementation

---

## DESIGN SYSTEM

### I. COLOR PALETTE

#### 1. Primary Colors - Cool Blue Tones

Used for branding, primary buttons, links, and key UI elements.

| Token | Hex Value | RGB | Usage |
|-------|-----------|-----|-------|
| primary-500 | #0ea5e9 | rgb(14, 165, 233) | Main brand color, links |
| primary-600 | #0284c7 | rgb(2, 132, 199) | Button hover, active states |
| gradient-primary | #0ea5e9 → #d946ef | - | Hero sections, CTAs |

#### 2. Secondary Colors - Vibrant Purple Accent

Used for highlights, badges, and secondary actions.

| Token | Hex Value | RGB | Usage |
|-------|-----------|-----|-------|
| secondary-500 | #d946ef | rgb(217, 70, 239) | Accent elements |

#### 3. Neutral Colors - Modern Grays

Used for backgrounds, text, borders, and UI hierarchy.

| Token | Hex Value | Usage |
|-------|-----------|-------|
| gray-50 | #f9fafb | Lightest backgrounds |
| gray-100 | #f3f4f6 | Secondary backgrounds |
| gray-200 | #e5e7eb | Borders |
| gray-300 | #d1d5db | Disabled states |
| gray-400 | #9ca3af | Placeholder text |
| gray-500 | #6b7280 | Secondary text |
| gray-600 | #4b5563 | Primary text |
| gray-700 | #374151 | Headings |
| gray-800 | #1f2937 | Dark backgrounds |
| gray-900 | #111827 | Darkest elements |

#### 4. Status Colors

| Context | Hex Value | Usage |
|---------|-----------|-------|
| Success | #22c55e | Success messages, active status |
| Warning | #f59e0b | Warnings, expiring soon |
| Error | #ef4444 | Error messages, validation |

### II. TYPOGRAPHY

#### 1. Font Family

Inter, -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif

#### 2. Font Sizes and Weights

| Element | Size | Weight | Line Height | Usage |
|---------|------|--------|-------------|-------|
| h1 | 2.25rem (36px) | 700 (Bold) | 1.2 | Page titles with gradient |
| h2 | 1.875rem (30px) | 700 (Bold) | 1.3 | Section headers |
| h3 | 1.5rem (24px) | 600 (Semi-bold) | 1.4 | Subsection headers |
| h4 | 1.25rem (20px) | 600 (Semi-bold) | 1.4 | Card titles |
| h5 | 1.125rem (18px) | 500 (Medium) | 1.5 | Small headers |
| h6 | 1rem (16px) | 500 (Medium) | 1.5 | Labels |
| body | 1rem (16px) | 400 (Normal) | 1.6 | Body text |
| small | 0.875rem (14px) | 400 (Normal) | 1.5 | Helper text |
| button | 0.875rem (14px) | 500 (Medium) | 1 | Button labels |

### 3. Spacing System

Based on 4px base unit:

| Token | Size | Pixels | Usage |
|-------|------|--------|-------|
| space-1 | 0.25rem | 4px | Tight spacing |
| space-2 | 0.5rem | 8px | Small gaps |
| space-3 | 0.75rem | 12px | Medium gaps |
| space-4 | 1rem | 16px | Standard spacing |
| space-5 | 1.25rem | 20px | Section spacing |
| space-6 | 1.5rem | 24px | Large gaps |
| space-8 | 2rem | 32px | Extra large |
| space-10 | 2.5rem | 40px | Section breaks |
| space-12 | 3rem | 48px | Major sections |
| space-16 | 4rem | 64px | Page sections |
| space-20 | 5rem | 80px | Hero spacing |

### 4. Border Radius

| Token | Value | Usage |
|-------|-------|-------|
| radius-sm | 4px | Small elements |
| radius-md | 8px | Buttons, inputs |
| radius-lg | 12px | Cards |
| radius-xl | 16px | Modals |
| radius-2xl | 24px | Large cards |
| radius-full | 9999px | Pills, avatars |

### III. COMPONENT STYLES

#### 1. Buttons

.btn-primary {
    background: linear-gradient(135deg, #0ea5e9, #0284c7);
    color: white;
    padding: 0.75rem 1.5rem;
    border-radius: 12px;
    font-weight: 500;
    transition: all 0.3s ease;
    box-shadow: 0 4px 6px rgba(14, 165, 233, 0.25);
}

.btn-primary:hover {
    transform: translateY(-2px);
    box-shadow: 0 8px 12px rgba(14, 165, 233, 0.35);
}

.btn-secondary {
    background: transparent;
    color: #0ea5e9;
    border: 2px solid #0ea5e9;
    padding: 0.75rem 1.5rem;
    border-radius: 12px;
}

.btn-gradient {
    background: linear-gradient(135deg, #667eea, #764ba2);
    color: white;
    padding: 1rem 2rem;
    border-radius: 24px;
}

#### 2. Cards

.card {
    background: rgba(255, 255, 255, 0.95);
    backdrop-filter: blur(10px);
    border-radius: 16px;
    border: 1px solid rgba(255, 255, 255, 0.18);
    box-shadow: 0 8px 32px rgba(31, 38, 135, 0.15);
    transition: all 0.3s ease;
}

.card:hover {
    transform: translateY(-4px);
    box-shadow: 0 12px 48px rgba(31, 38, 135, 0.25);
}

#### 3. Form Controls

.form-control {
    width: 100%;
    padding: 0.75rem 1rem;
    border: 2px solid #e5e7eb;
    border-radius: 8px;
    font-size: 1rem;
    transition: all 0.3s ease;
}

.form-control:focus {
    outline: none;
    border-color: #0ea5e9;
    box-shadow: 0 0 0 3px rgba(14, 165, 233, 0.1);
}

### 4. Animations

| Animation | Duration | Easing | Usage |
|-----------|----------|--------|-------|
| fadeInUp | 0.6s | ease-out | Page content entry |
| slideInRight | 0.4s | ease-out | Sidebar entry |
| pulse | 2s | infinite | CTA buttons |
| shimmer | 2s | linear infinite | Loading states |

### 5. Responsive Breakpoints

| Breakpoint | Min Width | Max Width | Target |
|------------|-----------|-----------|--------|
| xs | 0 | 479px | Mobile portrait |
| sm | 480px | 767px | Mobile landscape |
| md | 768px | 1023px | Tablet |
| lg | 1024px | 1279px | Desktop |
| xl | 1280px | 1535px | Large desktop |
| 2xl | 1536px | ∞ | Extra large |

---

## TROUBLESHOOTING

### COMMON ISSUES AND SOLUTIONS

#### I. Build and Compilation Issues

**Issue 1**: Build fails with CS0246 errors (type or namespace not found)

Solution:

1. Run: dotnet restore
2. Clean solution: dotnet clean
3. Rebuild: dotnet build

**Issue 2**: Build warnings about nullable properties

Solution:

Add nullable annotations to model properties:
public string? PropertyName { get; set; }
Or add 'required' modifier:
public required string PropertyName { get; set; }

**Issue 3**: Missing @using directive for components

Solution:

Add to _Imports.razor or at top of specific page:
@using JobBridge.Components.Common

#### II. Database Issues

**Issue 1**: SQLite database connection failed

Solution:

1. Check if jobbridge.db exists in project root
2. If missing, recreate:
   dotnet ef database update
3. Check connection string in appsettings.json

**Issue 2**: Migration errors

Solution:

1. Remove all migrations:
   dotnet ef migrations remove
2. Delete jobbridge.db
3. Create new migration:
   dotnet ef migrations add InitialCreate
4. Update database:
   dotnet ef database update

#### III. Authentication Issues

**Issue 1**: Cannot register new users

Solution:

1. Check if Identity is configured in Program.cs
2. Verify password requirements
3. Clear browser cookies
4. Check if email confirmation is disabled for development

**Issue 2**: Login redirects to wrong page

Solution:

1. Check Routes.razor for proper authorization attributes
2. Verify role-based redirects in Login.razor
3. Check MainLayout.razor for AuthorizeView components

#### IV. Runtime Issues

**Issue 1**: Port already in use

Solution:

1. Change ports in Properties/launchSettings.json
2. Or kill process using the port:
netstat -ano | findstr :7001
taskkill /PID [process_id] /F

**Issue 2**: CSS not loading properly

Solution:

1. Hard refresh browser (Ctrl+Shift+R)
2. Check if app.css exists in wwwroot/css/
3. Verify link tag in App.razor
4. Check for CSS isolation files (.razor.css)

**Issue 3**: JavaScript interop not working

Solution:

1. Check if interop.js is in wwwroot/js/
2. Verify script reference in App.razor
3. Ensure IJSRuntime is injected properly
4. Check browser console for errors

#### V. Performance Issues

**Issue 1**: Slow page loads

Solution:

1. Check database query efficiency
2. Implement pagination for large datasets
3. Use async/await properly
4. Consider caching frequently accessed data

**Issue 2**: Memory leaks with event handlers

Solution:

1. Implement IDisposable in components
2. Unsubscribe from events in Dispose method
3. Use CancellationToken for async operations

---

## API DOCUMENTATION

### ENDPOINTS OVERVIEW

#### 1. Authentication Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/auth/register` | Register new user | No |
| POST | `/api/auth/login` | User login | No |
| POST | `/api/auth/logout` | User logout | Yes |
| GET | `/api/auth/user` | Get current user | Yes |

#### 2. Job Post Endpoints
<!-- Add the endpoints here later!!!! -->

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/` | Get all active jobs | No |
| GET | `/api/` | Get specific job | No |
| POST | `/api/` | Create new job | Employer |
| PUT | `/api/` | Update job | Employer |
| DELETE | `/api/` | Delete job | Employer |
| GET | `/api/ | Search jobs | No |

#### 3. User Endpoints
<!-- Add the endpoints here later!!!! -->

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/` | Get user profile | Yes |
| PUT | `/api/` | Update profile | Yes |
| GET | `/api/` | Get bookmarks | Job Seeker |
| POST | `/api/` | Add bookmark | Job Seeker |
| DELETE | `/api/` | Remove bookmark | Job Seeker |

#### 4. Employer Endpoints
<!-- Add the endpoints here later!!!! -->

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/` | Get dashboard data | Employer |
| GET | `/api/` | Get employer's jobs | Employer |
| GET | `/api/` | Get job analytics | Employer |

---

### GIT WORKFLOW

#### I. Branch Naming

- Feature: `feature/description`
- Bug fix: `fix/description`
- Hotfix: `hotfix/description`
- Release: `release/version`

#### II. Commit Messages

## Format: `type`: `subject`

## Types

- feat: New feature
- fix: Bug fix
- docs: Documentation changes
- style: Code style changes
- refactor: Code refactoring
- test: Test additions/changes
- chore: Build/config changes

## Examples

git commit -m "feat: Add job bookmark functionality"
git commit -m "fix: Resolve login redirect issue"
git commit -m "docs: Update API documentation"

### III. Code Reviews

#### Checklist

- [ ] Code follows project conventions
- [ ] All tests pass
- [ ] No console errors
- [ ] Responsive design verified
- [ ] Accessibility standards met
- [ ] Security best practices followed
- [ ] Documentation updated
- [ ] Performance impact assessed

---

## TESTING

### 1. Unit Testing

#### Test Structure

Unit tests focus on testing individual components and services in isolation. Each service class has a corresponding test class that validates business logic, data transformations, and error handling. Tests use mocking frameworks to simulate dependencies and ensure components work correctly without external dependencies.

### 2. Integration Testing

#### Database Tests

Integration tests verify that the application correctly interacts with the database, including CRUD operations, data persistence, and query performance. These tests use an in-memory database to ensure data operations work correctly without affecting the production database.

### 3. UI Testing

#### Component Testing

UI tests validate that Blazor components render correctly, handle user interactions properly, and update state as expected. Tests verify form submissions, button clicks, navigation, and dynamic content updates work across different scenarios.

#### 4. Manual Test Cases

#### Test Case 1: User Registration

1. Navigate to /register
2. Fill in all required fields
3. Submit form
4. Verify email confirmation
5. Login with new credentials
6. Expected: Successful login and redirect to dashboard

#### Test Case 2: Job Search

1. Navigate to /jobs
2. Enter search keyword
3. Apply filters
4. Verify results match criteria
5. Click on job card
6. Expected: Detailed view displays correctly

#### Test Case 3: Responsive Design

1. Open application on desktop (1920x1080)
2. Resize to tablet (768x1024)
3. Resize to mobile (375x667)
4. Verify layout adjusts properly
5. Test navigation menu collapse
6. Expected: All breakpoints work correctly

### 5. Performance Testing

#### Load Testing Scenarios

- Concurrent users: 100
- Response time target: < 2 seconds
- Database queries: < 100ms
- Page load time: < 3 seconds

---

## DEPLOYMENT

### Azure Deployment Guide

#### STEP 0: Prerequisites

- Azure account with active subscription
- Azure CLI installed
- Visual Studio 2022 or VS Code with Azure extensions

#### Step 1: Prepare for Deployment

## Build in Release mode

dotnet build --configuration Release

## Test the release build

dotnet run --configuration Release

### Step 2: Create Azure Resources

## Login to Azure

az login

## Create resource group

az group create --name JobBridgeRG --location eastus

## Create App Service plan

az appservice plan create --name JobBridgePlan --resource-group JobBridgeRG --sku B1

## Create Web App

az webapp create --name JobBridgeApp --resource-group JobBridgeRG --plan JobBridgePlan

### Step 3: Configure Application Settings

## Set .NET version

az webapp config set --resource-group JobBridgeRG --name JobBridgeApp --net-framework-version v9.0

## Configure connection string

az webapp config connection-string set --resource-group JobBridgeRG --name JobBridgeApp --settings DefaultConnection="Data Source=jobbridge.db" --connection-string-type SQLite

### Step 4: Deploy Application

#### Option A: Using Visual Studio

1. Right-click project → Publish
2. Select Azure → Azure App Service
3. Select existing or create new
4. Configure and publish

#### Option B: Using Azure CLI

## Create deployment package

dotnet publish -c Release -o ./publish

## Zip the publish folder

Compress-Archive -Path ./publish/* -DestinationPath deploy.zip

## Deploy to Azure

az webapp deployment source config-zip --resource-group JobBridgeRG --name JobBridgeApp --src deploy.zip

### Step 5: Verify Deployment

## Get app URL

az webapp show --name JobBridgeApp --resource-group JobBridgeRG --query defaultHostName

## Browse to <!--Add the deployed app link later here. -->

### ENVIRONMENT CONFIGURATION

#### Development Settings (appsettings.Development.json)

The development configuration file contains settings optimized for local development and debugging. It includes:

- **Logging**: Set to "Information" level to capture detailed logs for debugging
- **Database**: Points to local SQLite database file (jobbridge.db)
- **Authentication**: Email confirmation disabled for easier testing
- **Debug Mode**: Detailed error messages and stack traces enabled

#### Production Settings (appsettings.json)

The production configuration file contains settings for the live environment with security and performance optimizations:

- **Logging**: Set to "Warning" level to reduce log verbosity and improve performance
- **Database**: Connection string for production database (can be SQLite or upgraded to SQL Server)
- **Authentication**: Email confirmation required for account verification
- **Security**: Sensitive information hidden, HTTPS enforced
- **Azure Integration**: SignalR service enabled for real-time features
- **Error Handling**: Generic error messages shown to users, detailed logs saved internally

#### Environment-Specific Overrides

Configuration settings can be overridden based on the environment:

- Use environment variables for sensitive data (API keys, connection strings)
- Azure App Service configuration takes precedence over file settings
- Development settings never deployed to production
- Secrets managed through Azure Key Vault or User Secrets in development

---

## SUPPORT AND RESOURCES

### Project Information

### Team 06 - CSE325 Team Project

- Course: CSE325 - Web Development
- Institution: Brigham Young University - Idaho
- Term: Summer 2025
- Project Duration: July 21 - August 12, 2025

### Team Members and Responsibilities

| Team Member | Primary Responsibility | Areas of Focus |
|-------------|----------------------|----------------|
| **Andrea Toreki** | UI/UX & Frontend Lead | Complete frontend development, responsive design system, project documentation, user interface components |
| **Amanda Cristina Schneider Migliorini** | Database Architect | Database design, backend development, data schema implementation |
| **Anana Agwu Ezikpe** | Project Setup & Auth Lead | Project initialization, authentication system architecture, user registration |
| **Emory Ryan Worsham** | Backend Developer | Database design, backend services, data layer implementation |
| **Ernest Nkansah Kyei** | Authentication Developer | Login/authentication features, user registration components |
| **Joseph Robert Williams** | Backend Developer | Database design, backend development, service layer |
| **Richard Boniface Sam** | Registration & QA | User registration system, testing procedures |
| **Steven Thomas** | Authentication Developer | Login/authentication implementation, user registration flow |
| **Yawogan Abraham Nyuiadzi** | Quality Assurance | Testing strategies, test execution, quality control |

### Project Links

- **Azure DevOps Project**: [https://dev.azure.com/atorekiTEAM/CSE325TeamProject]
- **Work Items Board**: [https://dev.azure.com/atorekiTEAM/CSE325TeamProject/_boards/board/t/CSE325TeamProject%20Team/Issues]
- **Pipeline**: [If configured we will add this later] <!-- Reminder for me: Ask the team about this later -->

### Development Timeline

#### Phase 1: Foundation (July 21-26, 2025)

- Blazor project structure setup
- Responsive design implementation
- Dashboard components
- Database models and Entity Framework
- User authentication system

#### Phase 2: Core Features (July 28 - August 2, 2025)

- Job bookmark system
- Job search and filtering
- Job listing display
- Job posting CRUD operations
- Login/Registration pages
- Project documentation

#### Phase 3: Refinement (August 4-9, 2025)

- Job listing validation
- Testing units
- Bug fixes and improvements
- Performance optimization
- Project Deployment
- Final testing

#### Phase 4: Delivery (August 11-12, 2025)

- Final project video recording
- Project presentation
- Submission

---

### GETTING HELP

#### I. For Developers

1. Check this documentation first
2. Review Azure DevOps work items
3. Contact the responsible team member
4. Post in team chat/discussion board

#### II. For Issues

- **Bug Reports**: Create work item in Azure DevOps or contact Richard or Yawogan (Testing/QA)
- **Feature Requests**: Discuss in team meeting first
- **UI/Frontend Issues**: Contact Andrea Toreki
- **Documentation Updates**: Contact Andrea Toreki
- **Database Issues**: Contact Amanda, Joseph, or Emory Ryan
- **Backend/API Issues**: Contact Amanda, Joseph, or Emory Ryan
- **Authentication/Login Issues**: Contact Anana, Ernest, or Steven
- **User Registration Issues**: Contact Anana, Ernest, Steven, or Richard
- **Testing/Quality Issues**: Contact Richard or Yawogan
- **Project Setup Issues**: Contact Anana (Project Initialization)

---

### EXTERNAL RESOURCES

#### I. Documentation

- [Blazor Documentation](https://docs.microsoft.com/aspnet/core/blazor)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [ASP.NET Core Identity](https://docs.microsoft.com/aspnet/core/security/authentication/identity)
- [SQLite Documentation](https://www.sqlite.org/docs.html)
- [Azure App Service](https://docs.microsoft.com/azure/app-service)

#### II. Learning Resources

- [Microsoft Learn - Blazor](https://learn.microsoft.com/training/modules/)
- [Blazor University](https://blazor-university.com/)
- [Entity Framework Tutorial](https://www.entityframeworktutorial.net/)

#### III. Tools

- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [Visual Studio Code](https://code.visualstudio.com/)
- [Azure DevOps](https://dev.azure.com/)
- [DB Browser for SQLite](https://sqlitebrowser.org/)
- [Postman](https://www.postman.com/) - API testing

---

### KNOWN ISSUES AND LIMITATIONS

#### I. Current Limitations

- No real-time notifications
- No direct messaging between users
- No integrated video interviews
- No payment processing
- No resume builder
- No company reviews/ratings
- Email notifications not implemented

#### II. Known Bugs

- Build warnings for nullable properties (non-critical)
- Component namespace warnings (add @using statements)
- Occasional session timeout issues
- Filter state not persisted on navigation

#### III. Future Enhancements (Post-MVP)

- Email notification system
- Advanced matching algorithm
- Resume parsing and analysis
- Mobile applications (iOS/Android)
- API for third-party integrations
- Advanced analytics dashboard
- Multi-language support
- Dark mode theme

---

### LICENSE AND COPYRIGHT

## I. Copyright © 2025 CSE325 Team 06

This project is developed for educational purposes as part of the CSE325 course at Brigham Young University - Idaho. All rights reserved.

**Third-Party Licenses**:

- ASP.NET Core - MIT License
- Entity Framework Core - MIT License
- Bootstrap - MIT License
- Inter Font - SIL Open Font License

### II. Version History

#### 1. Version 1.0.0 (August 12, 2025)

#### Initial Release

- Core authentication system
- Job posting CRUD operations
- Advanced search and filtering
- User dashboards (Job Seeker, Employer, Admin)
- Bookmark system
- Responsive design
- SQLite database integration
- Basic analytics

#### 2. Version 0.9.0 (August 9, 2025)

#### Beta Release

- All major features implemented
- UI/UX finalized
- Testing phase completed

#### 3. Version 0.5.0 (July 28, 2025)

#### Alpha Release

- Basic functionality working
- Database schema finalized
- Authentication implemented

---

## APPENDICES

### I. Glossary

| Term | Definition |
|------|------------|
| CRUD | Create, Read, Update, Delete operations |
| JWT | JSON Web Token for authentication |
| EF Core | Entity Framework Core ORM |
| Blazor Server | Server-side Blazor hosting model |
| SPA | Single Page Application |
| API | Application Programming Interface |
| DTO | Data Transfer Object |
| ORM | Object-Relational Mapping |
| WCAG | Web Content Accessibility Guidelines |
| CI/CD | Continuous Integration/Continuous Deployment |

### II. Configuration Files

#### launchSettings.json

This file configures how the application runs during development. It defines:

- **Launch Profiles**: Different configurations for running the app (HTTP/HTTPS)
- **Port Settings**: Local development ports (typically 7001 for HTTPS, 5000 for HTTP)
- **Browser Launch**: Automatically opens browser when application starts
- **Environment Variables**: Sets development environment for proper configuration loading
- **Debugging Options**: Enables detailed error messages and hot reload features

#### III. .gitignore

The gitignore file specifies which files and folders should not be tracked by version control:

- **Build Outputs**: Excludes bin/ and obj/ folders containing compiled code
- **Database Files**: Ignores temporary SQLite files
- **IDE Settings**: Excludes Visual Studio (.vs/) and VS Code (.vscode/) configuration
- **User-Specific Files**: Ignores personal settings
- **Publish Folder**: Excludes deployment packages and published outputs
- **Sensitive Data**: Ensures no credentials or secrets are accidentally committed

### IV. Sample Data for Testing

The application includes seed data for testing purposes that populates:

- **Sample Employers**: Creates test companies across different industries (Technology, Healthcare, Finance) with various locations
- **Sample Job Posts**: Generates diverse job listings with different employment types (Full-time, Part-time, Remote) and realistic job descriptions
- **Test Users**: Creates sample accounts for each role (Job Seeker, Employer, Admin) to test different access levels
- **Mock Applications**: Populates application history and bookmarks for testing user workflows
- **Statistical Data**: Generates view counts and engagement metrics for dashboard testing

This seed data is automatically loaded when the database is initialized in development mode, providing a realistic testing environment without manual data entry.

---

## End of Documentation

**Document Version**: 1.0 Final  
**Last Updated**: August 6, 2025  
**Total Pages**: Comprehensive Guide  
**Status**: In Progress
<!-- REMINDER: Change the status once the final project is done and the final version of the documentation is 100% ready. -->