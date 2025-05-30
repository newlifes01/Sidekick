using Sidekick.Apis.Poe.Parser.Properties.Filters;
using Sidekick.Apis.Poe.Trade.Filters;

namespace Sidekick.Apis.Poe.Trade.Requests.Filters;

public class StatFilterValue
{
    public StatFilterValue()
    {
    }

    public StatFilterValue(string? option)
    {
        Option = option;
    }

    public StatFilterValue(IntPropertyFilter filter)
    {
        Option = filter.Checked ? "true" : "false";
        Min = filter.Min;
        Max = filter.Max;
    }

    public StatFilterValue(DoublePropertyFilter filter)
    {
        Option = filter.Checked ? "true" : "false";
        Min = filter.Min;
        Max = filter.Max;
    }

    public StatFilterValue(ModifierFilter filter)
    {
        Option = filter.Line.OptionValue;
        Min = filter.Min;
        Max = filter.Max;
    }

    public object? Option { get; set; }

    public double? Min { get; set; }

    public double? Max { get; set; }

    public double? Weight { get; set; }
}
