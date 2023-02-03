using MonksInn.Domain;
using MonksInn.Domain.Interfaces;
using MonksInn.Logic.Extensions;
using MonksInn.Logic.Models.DeliverySlotLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonksInn.Logic
{
    public class DeliverySlotLogic : BaseLogic
    {
        public DeliverySlotLogic(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<DeliverySlot> GetAllSlots()
        {
            return Uow.DbContext.DeliverySlots.AsQueryable(true);
        }

        public DeliverySlot Add(DeliverySlot deliverySlot)
        {
            deliverySlot.DateCreated = DateTime.Now;
            return Uow.DbContext.DeliverySlots.Add(deliverySlot);
        }

        public void Delete(Guid id)
        {
            var deliveryslot = Uow.DbContext.DeliverySlots.AsQueryable(true).FirstOrDefault(a => a.Id == id);
            if (deliveryslot != null)
            {
                deliveryslot.IsArchived = true;
            }
        }

        public DeliverySlot GetDeliverySlot(Guid id)
        {
            return Uow.DbContext.DeliverySlots.AsQueryable(true).FirstOrDefault(a => a.Id == id);
        }
        /// <summary>
        /// Gets the Available DeliverySlots. Also creates delivery slots for the next days. requires a db savechanges
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public List<DeliveryDateAllocation> GenerateAndFetchNextAvailableDeliverySlots(int days)
        {
            var resultList = new List<DeliveryDateAllocation>();


            var allAllocations = Uow.DbContext.DeliveryDateAllocations.AsQueryable(true).ToList();
            var currentDeliverySlots = Uow.DbContext.DeliverySlots.AsQueryable(true).ToList();
            var workingDate = DateTime.Now;

            for (int i = 0; i < days; i++)
            {
                var allocationsForDate = allAllocations.Where(a => a.DateAllocation.Date == workingDate.Date).ToList();
                var slotsForDate = currentDeliverySlots.Where(a => a.DayOfWeek == workingDate.DayOfWeek);

                // create new allocations if they dont exist
                foreach (var slot in slotsForDate)
                {
                    var allocationForSlot = allocationsForDate.Where(a => a.DayOfWeek == slot.DayOfWeek && a.CommitedStartHour == slot.StartTime && a.CommitedEndHour == slot.EndTime).FirstOrDefault();
                    if (allocationForSlot == null)
                    {
                        var newAllocation = Uow.DbContext.DeliveryDateAllocations.Add(new DeliveryDateAllocation()
                        {
                            DateCreated = DateTime.Now,
                            DayOfWeek = slot.DayOfWeek,
                            CommitedStartHour = slot.StartTime,
                            CommitedEndHour = slot.EndTime,
                            DateAllocation = workingDate.Date,
                        });
                        allocationsForDate.Add(newAllocation);
                    }
                }

                resultList.AddRange(allocationsForDate);


                workingDate = workingDate.AddDays(1);
            }


            return resultList
                .OrderBy(a => a.DateAllocation)
                .ThenBy(a => a.CommitedStartHour)
                .ThenBy(a => a.CommitedEndHour)
                .ToList();
        }
    }
}
