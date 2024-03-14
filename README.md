# SolviaWinUnattend

SolviaWinUnattend is a CLI tool developed by Christian Casutt at Solvia GmbH, Switzerland, designed to simplify the creation of Windows unattended installation ISOs. The tool allows users to choose between BIOS with MBR or UEFI with GPT settings for their installation media, and it automatically integrates a corresponding `autounattend.xml` file into the ISO. This utility is particularly useful for creating custom Windows installation media that automates the installation process, tailored for use in various environments, including older ESX versions that only support BIOS with MBR.

## Features

- Easy selection between BIOS/MBR and UEFI/GPT or UEFI with manual partitioning for Windows installations.
- Automatically handles the creation and integration of the appropriate `autounattend.xml` file into an ISO.
- Option to delete, rename, or exit if the target ISO file already exists, preventing accidental overwrites.
- Generates a `readme.txt` file on the fly, including a custom ASCII art logo, contact information, and current date, bundled into the ISO.

## Requirements

- .NET Runtime compatible with the project's target framework (check project settings for exact version).
- DiscUtils.Iso9660 library for ISO manipulation.
- Basic understanding of Windows unattended installation processes and XML configuration.

## Installation

To get started with SolviaWinUnattend, follow these steps:

1. Ensure you have the .NET Runtime installed on your system.
2. Clone the repository to your local machine using `git clone https://github.com/itsChris/SolviaWinUnattend.git`.
3. Navigate into the cloned repository's directory.
4. Build the project using the .NET CLI with `dotnet build`.
5. Run the application with `dotnet run`.

## Usage

After launching the application, follow the on-screen prompts to choose your preferred installation configuration. The program will guide you through the process of selecting the type of installation (BIOS/MBR, UEFI/GPT, or UEFI with manual partitioning), and it will handle the rest, including the creation of the ISO file.

Configuration Details
The autounattend-UEFI-GPT.xml is pre-configured to support installations on UEFI systems with a GPT partition table. This XML file is part of the SolviaWinUnattend tool and automates various steps of the Windows installation process, including but not limited to:

Disk partitioning tailored for UEFI systems.
Input of product keys and license terms acceptance.
Creation of user accounts and setting regional preferences.
Application of network settings and joining of domains, if applicable.
This automated installation configuration file is designed to streamline the deployment of Windows operating systems in enterprise environments, reducing manual setup time and potential for human error.

Important Note
The provided XML configurations, such as autounattend-UEFI-GPT.xml, are examples of what can be achieved with the SolviaWinUnattend tool. Users are encouraged to customize these files according to their specific installation requirements and policies. The tool supports various configurations including BIOS/MBR setups, UEFI/GPT, and UEFI with manual partitioning, each with its respective autounattend.xml template.

## Contributing

Contributions to SolviaWinUnattend are welcome. If you have improvements or bug fixes, please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or fix.
3. Commit your changes with clear, descriptive commit messages.
4. Push your changes to your fork.
5. Submit a pull request against the `main` branch of `https://github.com/itsChris/SolviaWinUnattend`.

Please ensure your code adheres to the project's coding standards and includes appropriate tests (if applicable).

## License

Free to use, modify, and distribute under the terms of the MIT license.

## Contact

For any inquiries, please contact us at:

- Website: [https://www.solvia.ch](https://www.solvia.ch)


