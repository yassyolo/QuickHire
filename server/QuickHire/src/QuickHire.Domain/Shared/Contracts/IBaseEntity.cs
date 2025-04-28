namespace QuickHire.Domain.Shared.Contracts;
internal interface IBaseEntity<TId> 
{
	TId Id { get; set; }
}