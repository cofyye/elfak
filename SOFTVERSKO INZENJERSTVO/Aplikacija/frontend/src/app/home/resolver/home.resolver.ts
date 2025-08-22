import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from "@angular/router";
import { HomeService, IUser } from "../services/home.service";
import { IDataAcceptResponse } from "../../shared/interfaces/response.interface";
import { inject } from "@angular/core";

export const homeCarouselResolver: ResolveFn<
  IDataAcceptResponse<IUser[]>
> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  return inject(HomeService).getBestExperts();
};