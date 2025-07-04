﻿using QuickHire.Application.Admin.Models.Users.Notifications;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Notifications.GetNotifications;

public record GetNotificationsQuery(bool Buyer) : IQuery<IEnumerable<GetNotificationsResponseModel>>;
