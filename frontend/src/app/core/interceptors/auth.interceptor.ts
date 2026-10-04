import { HttpInterceptorFn } from "@angular/common/http";
import { KeycloakService } from "../services/keycloak.service";
import { inject } from "@angular/core";

export const authInterceptor: HttpInterceptorFn = (req, next) => 
{
  const keycloakService = inject(KeycloakService);

  const token = keycloakService.getAccessToken();

  if (!token) 
  {
    return next(req);
  }

  const authReq = req.clone({setHeaders: { Authorization: `Bearer ${token}`}});

  return next(authReq);
};