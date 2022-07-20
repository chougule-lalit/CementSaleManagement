import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import {CommonService} from "../services/common.service";

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private router: Router, private commonService: CommonService) {
  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): any {

    // let roles = '';
    // this.commonService.getRoleDropdown().subscribe((result) => {
    //   console.log('Roles in Role Guard : ', result);
    //   if (JSON.parse(localStorage.getItem('user-details')!).role === result.id) {
    //     roles = result.name;
    //   }
    // })

    // console.log('Roles in Role Guard Called : ', route.data.expectedRoles, JSON.parse(localStorage.getItem('user-details')!).role);
    if (route.data.expectedRoles == JSON.parse(localStorage.getItem('user-details')!).role) {
      return true;
    }
    alert('Access Denied!');
    return false;
  }
}
