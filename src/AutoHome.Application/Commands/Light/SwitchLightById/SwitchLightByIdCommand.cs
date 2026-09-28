namespace AutoHome.Application.Commands;

public sealed record SwitchLightByIdCommand(Guid Id, bool On);