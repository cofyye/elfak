import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from "@angular/router";
import { IProfileGet } from "../interfaces/profile.interface";
import { inject } from "@angular/core";
import { ProfileService } from "../services/profile.service";
import { IDataAcceptResponse } from "../../../../shared/interfaces/response.interface";

export const profileResolver: ResolveFn<IDataAcceptResponse<IProfileGet>> = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
    return inject(ProfileService).getProfileInfo();
}