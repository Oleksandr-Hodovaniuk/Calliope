using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Tracks.DTOs;

public record CreateTrackDto(
    string Name,
    string? Author,
    string? Album,
    byte? Rating
);
