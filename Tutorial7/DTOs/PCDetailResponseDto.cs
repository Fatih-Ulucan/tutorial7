namespace Tutorial7.DTOs
{
    public class PCDetailResponseDto : PCResponseDto
    {
        public List<PCComponentDto> Components { get; set; } = new List<PCComponentDto>();
    }

    public class PCComponentDto
    {
        public int Amount { get; set; }
        public ComponentDetailDto Component { get; set; } = null!;
    }

    public class ComponentDetailDto
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ManufacturerDto Manufacturer { get; set; } = null!;
        public TypeDto Type { get; set; } = null!;
    }

    public class ManufacturerDto
    {
        public int Id { get; set; }
        public string Abbreviation { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string FoundationDate { get; set; } = null!;
    }

    public class TypeDto
    {
        public int Id { get; set; }
        public string Abbreviation { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}