﻿using System;
using System.Collections.Generic;

namespace OurBeautyReferralNetwork.DataTransferObjects;

public partial class RegisterCustomerDTO
{
    public string PkCustomerId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public string Email { get; set; } = null!;

    public bool Vip { get; set; }

    public bool Confirm18 { get; set; }

    public string Password { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;

    public string? FkReferralId { get; set; }
}
