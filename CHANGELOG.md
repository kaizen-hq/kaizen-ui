# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.0.5] - Unreleased

### Added
- `GridColumnHeader` now supports `ChildContent` to allow custom content rendering, defaulting to the column name
- `ToolbarButton` now supports `IsActive` parameter that displays a check icon when active

### Fixed
- `Paginator` and `DataGrid` pagination calculation was incorrect due to integer division
- `DataGrid` no longer resets to page 0 when parameters change; only resets if current page is out of bounds

## [0.0.4] - 2026-02-16

### Added
- `DataGrid` now supports `RowDataTestId` parameter to specify a custom `data-test-id` attribute for each row

## [0.0.3] - 2026-01-09

### Fixed
- `DateRangePicker` unable to navigate to previous months
- New default styles for `select` element
- Fixed enter key not saving open rows past the first row

## [0.0.2]

### Fixed
- `NavBarLink` now correctly toggles menu open/close on desktop
- Improved mobile navigation experience

## [0.0.1]

### Initial Release
