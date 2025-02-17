# Discord DnD Chronicles

Discord DnD Chronicles is a tool designed specifically for managing and exporting Dungeons & Dragons campaigns played on Discord. It imports Discord JSON export files, categorizes channels (Roleplay, Out-of-Character, and Assets), and transforms the data into a narrative-friendly format. Built with a modular architecture, the core logic is decoupled from the UI, enabling future support for a CLI or API interface.

## Features

- **Incremental Import:**  
  Automatically import new or updated messages from Discord JSON exports, ensuring that only new content is added to your campaign's database.

- **Multi-Campaign Support:**  
  Manage multiple DnD campaigns, each with its own set of locations, characters, and messages.

- **Channel Categorization:**  
  Automatically classify channels into Roleplay, OOC, and Assets based on naming conventions (e.g., channels that correspond to in-game locations).

- **User & Character Mapping:**  
  Extract Discord users and map them to campaign characters. Characters can be either Player Characters (PCs) or Non-Player Characters (NPCs, managed by the DM) and can be organized into groups (e.g., guilds, vendors).

- **Location & Map Management:**  
  Associate channels with in-game locations (such as cities or dungeons) and attach maps (via message attachments). Automatically derive location names from channel names and generate chapters based on locations, with the option to adjust chapters manually.

- **Statistics & Timeline:**  
  Calculate campaign statistics (e.g., campaign duration, number of fights, average rounds per fight, messages per character) and generate an in-game timeline that reflects narrative time progression and character activity.

- **Search & Context Navigation:**  
  Integrated search functionality allows you to find messages with context (displaying preceding and following messages) and navigate the full narrative without requiring an export first.

## Architecture

Discord DnD Chronicles is built on .NET 8+ and leverages:
- **Entity Framework Core (with SQLite):** For robust data persistence of campaigns, channels, messages, locations, and character mappings.
- **Microsoft.Extensions.Logging:** For enterprise-level logging.
- **Microsoft.Extensions.DependencyInjection & Configuration:** For dependency injection and centralized configuration management.
- **System.Text.Json:** For efficient JSON parsing.

### Optional & Future Libraries
- **System.CommandLine:** For adding a CLI interface.
- **CommunityToolkit.Mvvm:** To streamline MVVM in the Windows UI.
- **SQLite FTS:** For advanced full-text search capabilities if needed.

The core functionality is encapsulated in a class library that can be used by multiple presentation layers (Windows UI, CLI, API).

## Getting Started

### Prerequisites
- [.NET 8+ SDK](https://dotnet.microsoft.com/download)
- SQLite (or your chosen EF Core provider)

### Installation
1. **Clone the Repository:**

       git clone https://github.com/yourusername/DiscordDnDChronicles.git
       cd DiscordDnDChronicles

2. **Restore Dependencies:**

       dotnet restore

3. **Build the Solution:**

       dotnet build

### Configuration
Edit the `appsettings.json` file to configure settings such as the database connection string, logging preferences, and any other application-specific parameters.

### Running the Application
Launch the Windows UI project (or the CLI/API, as available) using:

       dotnet run --project ./src/DiscordDnDChronicles.UI

## Usage

- **Importing Data:**  
  Import your Discord JSON export files. The application will automatically add only new or updated messages.

- **Mapping Users & Characters:**  
  Utilize the user mapping interface to assign Discord users to campaign characters (PCs or NPCs) and group them as needed.

- **Managing Locations & Chapters:**  
  Channels are automatically linked to in-game locations (based on channel names) to create chapters. You can manually adjust these chapters if needed.

- **Viewing Statistics & Timeline:**  
  View detailed campaign statistics and an in-game timeline to track narrative progression and character participation.

- **Search Functionality:**  
  Use the integrated search to locate messages with full context (showing messages before and after the result) and navigate through the campaign narrative.

## Roadmap & Future Work

- **CLI & API Integration:**  
  Expand the application with a command-line interface and/or RESTful API.

- **Enhanced Combat Analysis:**  
  Add features to annotate and analyze combat sequences (fight events, rounds).

- **UI Enhancements:**  
  Refine the story reader and timeline editor for an improved user experience.

## Contributing

Contributions are welcome! Please see our CONTRIBUTING.md for guidelines on how to get started.

## License

This project is licensed under the MIT License. See the LICENSE file for details.
