using System;
using System.Collections.Generic;
using System.Text;

using TurfTankRegistrationApplication.Connection;

namespace TurfTankRegistrationApplication.Model
{
    /// <summary>
    /// Implements a validateable objects own API and validate method.
    /// </summary>
    public interface IValidateable
    {
        /// <summary>
        /// Validates that the given object is on a format which is compatible with the database.
        /// 1. Does it have a valid SerialNumber or QR ID?
        /// 2. If it contains a reference, are all the references properties set?
        /// 3. Throws a ValidationException if it didn't validate correctly.
        /// </summary>
        /// <param name="idRestriction">A restriction for which IDs are valid in different states of the program</param>
        /// <helper>Example: During Preregistration it is important that the given component doesn't have a serial number. If it has one, cannot be preregistered (it is already registered!)</helper>
        void ValidateSelf(SerialOrQR idRestriction = SerialOrQR.AnyId);
    }

    /// <summary>
    /// An Enum used to restrict the ValidateSelf method to make sure a specific condition for the ID is set.
    /// </summary>
    public enum SerialOrQR
    {
        AnyId,
        NoId,
        OnlySerialId,
        OnlyQRId,
        BothSerialAndQRId
    }
}
